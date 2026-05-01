use rayon::prelude::*;
use std::io::{self, Read};

const GAP_PENALTY: i32 = -4;

fn main() {
    // --------------------
    // ---- READ INPUT ----
    // --------------------

    let mut input = String::new();
    io::stdin()
        .read_to_string(&mut input)
        .expect("Failed to read input");

    let first_line = input.lines().next().unwrap();
    let headers: Vec<u8> = first_line
        .split_whitespace()
        .map(|s| s.as_bytes()[0])
        .collect();

    let k = headers.len();
    let mut tokens = input.split_whitespace();

    // Skip the headers that were already processed in the iterator
    for _ in 0..k {
        tokens.next();
    }

    let mut cost_matrix = [0i32; 256 * 256];
    for &row_char in &headers {
        for &col_char in &headers {
            let val = tokens.next().unwrap().parse::<i32>().unwrap(); // Parse into i32
            cost_matrix[(row_char as usize * 256) + col_char as usize] = val;
        }
    }

    let q: usize = tokens.next().unwrap().parse().unwrap();

    let queries: Vec<(&[u8], &[u8])> = (0..q)
        .map(|_| {
            let s1 = tokens.next().unwrap().as_bytes();
            let s2 = tokens.next().unwrap().as_bytes();
            (s1, s2)
        })
        .collect();

    // ---------------------
    // --- SOLVE PROBLEM ---
    // ---------------------

    let results: Vec<(String, String)> = queries
        .into_par_iter()
        .map_init(
            || vec![0i32; 4096 * 4096],
            |word_grid, (s1, s2)| {
                // Because the grid is reused, we ensure the 0th position is reset.
                // The algorithm overwrites all other required bounds.
                word_grid[0] = 0;

                for i in 1..=s1.len() {
                    word_grid[i * 4096] = word_grid[(i - 1) * 4096] + GAP_PENALTY;
                }

                for j in 1..=s2.len() {
                    word_grid[j] = word_grid[j - 1] + GAP_PENALTY;
                }

                for i in 1..=s1.len() {
                    let row_offset = i * 4096;
                    let prev_row_offset = (i - 1) * 4096;

                    for j in 1..=s2.len() {
                        let char_cost =
                            cost_matrix[(s1[i - 1] as usize * 256) + s2[j - 1] as usize];
                        let diagonal = word_grid[prev_row_offset + (j - 1)] + char_cost;
                        let down = word_grid[prev_row_offset + j] + GAP_PENALTY;
                        let right = word_grid[row_offset + (j - 1)] + GAP_PENALTY;

                        word_grid[row_offset + j] = diagonal.max(down).max(right);
                    }
                }

                let max_len = s1.len() + s2.len();
                let mut aligned_s1: Vec<u8> = Vec::with_capacity(max_len);
                let mut aligned_s2: Vec<u8> = Vec::with_capacity(max_len);

                {
                    let mut i = s1.len();
                    let mut j = s2.len();

                    let score = |r: usize, c: usize| word_grid[r * 4096 + c];
                    let cost = |a: u8, b: u8| cost_matrix[(a as usize * 256) + b as usize];

                    while i > 0 || j > 0 {
                        if i > 0
                            && j > 0
                            && score(i, j) == score(i - 1, j - 1) + cost(s1[i - 1], s2[j - 1])
                        {
                            aligned_s1.push(s1[i - 1]);
                            aligned_s2.push(s2[j - 1]);
                            i -= 1;
                            j -= 1;
                        } else if i > 0 && score(i, j) == score(i - 1, j) + GAP_PENALTY {
                            aligned_s1.push(s1[i - 1]);
                            aligned_s2.push(b'*');
                            i -= 1;
                        } else {
                            aligned_s1.push(b'*');
                            aligned_s2.push(s2[j - 1]);
                            j -= 1;
                        }
                    }
                }

                aligned_s1.reverse();
                aligned_s2.reverse();

                let s1_str = String::from_utf8(aligned_s1).unwrap();
                let s2_str = String::from_utf8(aligned_s2).unwrap();

                (s1_str, s2_str)
            },
        )
        .collect();

    // ---------------------
    // --- OUTPUT RESULT ---
    // ---------------------

    for (s1_res, s2_res) in results {
        println!("{} {}", s1_res, s2_res);
    }
}
