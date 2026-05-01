cargo build --release --manifest-path gorilla_rs/Cargo.toml --target-dir gorilla_rs/target
time ./check_solution.sh ./gorilla_rs/target/release/gorilla_rs
