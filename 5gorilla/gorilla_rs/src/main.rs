use std::io::{self, Read};

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

    let mut word_grid = [0i32; 3501 * 3501];

    for (s1, s2) in queries {
        // Calculate the values for the grid
        for i in 0..s1.len() {
            for j in 0..s2.len() {
                let idx = i * 3501 + j;

                let cost = cost_matrix[(s1[i] as usize * 256) + s2[j] as usize];
                word_grid[idx] = cost;
            }
        }

        // Path-find through the grid
        let mut sum = 0;
        let mut current_location = 0;
        let directions = [1, 4096, 4096];
        while (current_location / 4096) + 1 < s1.len() && (current_location % 4096) + 1 < s2.len() {
            let (min_val, chosen_dir) = directions
                .into_iter()
                .map(|dir| (word_grid[current_location + dir], dir))
                .min_by_key(|&(val, _dir)| val)
                .unwrap();

            sum += min_val;
            current_location += chosen_dir;
        }

        // Output result
        println!("{}", sum);
    }
}
