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
        let s1 = tokens.next().unwrap();
        let s2 = tokens.next().unwrap();
        (s1, s2)
    });

    // ---------------------
    // --- SOLVE PROBLEM ---
    // ---------------------

    for (_s1, _s2) in queries {
        // RUN DP ALGORITHM HERE
    }
}
