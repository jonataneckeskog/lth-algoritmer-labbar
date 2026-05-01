cargo build --manifest-path gorilla_rs/Cargo.toml --target-dir gorilla_rs/target
./check_solution.sh ./gorilla_rs/target/debug/gorilla_rs
