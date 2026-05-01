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

    let queries = (0..q).map(|_| {
        let s1 = tokens.next().unwrap().as_bytes();
        let s2 = tokens.next().unwrap().as_bytes();
        (s1, s2)
    });

    // ---------------------
    // --- SOLVE PROBLEM ---
    // ---------------------

    let mut word_grid = vec![0i32; 4096 * 4096];
    word_grid[0] = 0;

    for (s1, s2) in queries {
        //
        // Calculate the values for the grid
        //

        // Initialize the first row
        for i in 1..=s1.len() {
            word_grid[i * 4096] = word_grid[(i - 1) * 4096] + GAP_PENALTY;
        }

        // Initialize the first column
        for j in 1..=s2.len() {
            word_grid[j] = word_grid[j - 1] + GAP_PENALTY;
        }

        // Initialize the rest of the grid
        for i in 1..=s1.len() {
            let row_offset = i * 4096;
            let prev_row_offset = (i - 1) * 4096;

            for j in 1..=s2.len() {
                // MATCH/MISMATCH logic
                let char_cost = cost_matrix[(s1[i - 1] as usize * 256) + s2[j - 1] as usize];
                let diagonal = word_grid[prev_row_offset + (j - 1)] + char_cost; // Previous cost + char cost

                // GAP logic
                let down = word_grid[prev_row_offset + j] + GAP_PENALTY;
                let right = word_grid[row_offset + (j - 1)] + GAP_PENALTY;

                // Maximize the yield
                word_grid[row_offset + j] = diagonal.max(down).max(right);
            }
        }

        //
        // Calculate the values for the grid
        //

        let max_len = s1.len() + s2.len();
        let mut aligned_s1: Vec<u8> = Vec::with_capacity(max_len);
        let mut aligned_s2: Vec<u8> = Vec::with_capacity(max_len);

        {
            let mut i = s1.len();
            let mut j = s2.len();

            let score = |r: usize, c: usize| word_grid[r * 4096 + c];
            let cost = |a: u8, b: u8| cost_matrix[(a as usize * 256) + b as usize];

            while i > 0 || j > 0 {
                if i > 0 && j > 0 && score(i, j) == score(i - 1, j - 1) + cost(s1[i - 1], s2[j - 1])
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

        let s1_str = std::str::from_utf8(&aligned_s1).unwrap();
        let s2_str = std::str::from_utf8(&aligned_s2).unwrap();

        // Output result
        println!("{} {}", s1_str, s2_str);
    }
}
