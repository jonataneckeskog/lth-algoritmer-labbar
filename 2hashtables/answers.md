## Lab 2 - Hashtables

### 4.
The suitable number of list nodes is considered to be between 25% to 75% of the number of spaces avaliable in the array. Keeping it below 25% essentially wastes memory unnessesarily, while above 75% start to significantly slow down the performance.

### 7.
With a power-of-two capacity, quadratic probing using $i^2$ only explores a small fraction of the table (e.g., $i^2 \pmod{16}$ yields only offsets 0, 1, 4, 9). When $\alpha > 0.5$, collisions cause the probe sequence to exhaust these limited offsets without finding an empty spot. My code's `for` loop would then silently end and overwrite an existing item. The fact that tests still evaluated to "Correct!" despite this silent data loss perfectly illustrates the danger of "just testing"—the tests passed because the specific inputs didn't trigger enough collisions on the most frequent words to affect the final output, effectively hiding a severe flaw.

### 8. Benchmark table

| Probing Strategy            | Max Load ($\alpha$) | data/secret/6big.in | data/secret/7huge.in | Notes                                                                                    |
| :-------------------------- | :------------------ | :------------------ | :------------------- | :--------------------------------------------------------------------------------------- |
| **Linear**                  | 0.3                 | *Timeout*           | *Timeout*            | Excessive resizing/reallocating                                                          |
| **Linear**                  | 0.5                 | 132 ms              | 550 ms               | Baseline performance                                                                     |
| **Linear**                  | 0.75                | 182 ms              | 681 ms               | Noticeable slowdown (Clustering)                                                         |
| **Linear**                  | 1.0                 | 256 ms              | 810 ms               | Significant performance hit                                                              |
| **Quadratic (Unoptimized)** | 0.5                 | 135 ms              | 614 ms               |                                                                                          |
| **Quadratic (Unoptimized)** | 0.75                | 148 ms              | 621 ms               | Random failures (see note below)                                                         |
| **Quadratic (Unoptimized)** | 1.0                 | 144 ms              | 629 ms               | Random failures (see note below)                                                         |
| **Quadratic (Optimized)**   | 0.5                 | 125 ms              | 620 ms               |                                                                                          |
| **Quadratic (Optimized)**   | 0.75                | 151 ms              | 645 ms               | Slightly worse than the Unoptimized version, likely a cause of inconsistant benchmarking |
| **Quadratic (Optimized)**   | 1.0                 | 132 ms              | 574 ms               |                                                                                          |
| **Triangular (Optimized)**  | 0.75                | 129 ms              | 561 ms               |                                                                                          |

*note:* The Quadratic Probing methods were prone to experiencing random errors in case c# happened to generate 'unlucky' hashes for the strings, especially for higher maximum thresholds. This meant that some test runs just randomly failed. The results are presented for the cases when it did work.

### 9. What takes the most time?
Resizing the arrays and rehashing all elements (which requires allocating large memory blocks) takes a substantial amount of time. In linear probing with a high max load factor ($\alpha$), primary clustering also becomes a major bottleneck since finding an empty slot degrades to an $O(N)$ operation. Additionally, division and remainder operations (`%`) are relatively slow at the hardware level.

### 10. Modulo Optimization
By replacing the modulo operator `% _capacity` with the bitwise AND operator `& (_capacity - 1)`, we can compute the remainder much faster. This works because `_capacity` is always maintained as a power of 2. Applying this micro-optimization reduces CPU cycles for every hash and probe operation.

### Benchmarks

#### LinearProbing, no optimization

##### 0.75
data/secret/6big.in
Time elapsed: 182 ms
Correct!
data/secret/7huge.in
Time elapsed: 681 ms
Correct!

##### 0.5
data/secret/6big.in
Time elapsed: 132 ms
Correct!
data/secret/7huge.in
Time elapsed: 550 ms
Correct!

##### 1.0
data/secret/6big.in
Time elapsed: 256 ms
Correct!
data/secret/7huge.in
Time elapsed: 810 ms
Correct!

##### 0.3
I waited a few minutes, but 6big.in took way too long. Having such a low maximum causes a complete resizing/reallocation of the entire table over and over.

#### QuadraticProbing, no optimization

##### 0.75
Inconsistant results. Sometimes failed, sometimes succeeded. This has to do with how c# uses a non-deterministic Hash functions. At any case, when it DID succeed:

data/secret/6big.in
Time elapsed: 148 ms
Correct!
data/secret/7huge.in
Time elapsed: 621 ms
Correct!
jonatanec

##### 0.5
data/secret/6big.in
Time elapsed: 135 ms
Correct!
data/secret/7huge.in
Time elapsed: 614 ms
Correct!

##### 1.0
Once again, sometimes succeeded, sometimes did not.

data/secret/6big.in
Time elapsed: 144 ms
Correct!
data/secret/7huge.in
Time elapsed: 629 ms
Correct!

#### QuadraticProbing, optimized

##### 0.75
data/secret/6big.in
Time elapsed: 151 ms
Correct!
data/secret/7huge.in
Time elapsed: 645 ms
Correct!

##### 0.5
data/secret/6big.in
Time elapsed: 125 ms
Correct!
data/secret/7huge.in
Time elapsed: 620 ms
Correct!

##### 1.0
data/secret/6big.in
Time elapsed: 132 ms
Correct!
data/secret/7huge.in
Time elapsed: 574 ms
Correct!

#### TriangularProbing, optimized

##### 0.75
data/secret/6big.in
Time elapsed: 129 ms
Correct!
data/secret/7huge.in
Time elapsed: 561 ms
Correct!
