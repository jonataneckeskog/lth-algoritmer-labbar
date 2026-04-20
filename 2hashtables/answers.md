## Lab 2 - Hashtables

### 4.
The suitable number of list nodes is considered to be between 25% to 75% of the number of spaces avaliable in the array. Keeping it below 25% essentially wastes memory unnessesarily, while above 75% start to significantly slow down the performance.

### 7.
With a power-of-two capacity, quadratic probing using $i^2$ only explores a small fraction of the table (e.g., $i^2 \pmod{16}$ yields only offsets 0, 1, 4, 9). When $\alpha > 0.5$, collisions cause the probe sequence to exhaust these limited offsets without finding an empty spot. My code's `for` loop would then silently end and overwrite an existing item. The fact that tests still evaluated to "Correct!" despite this silent data loss perfectly illustrates the danger of "just testing"—the tests passed because the specific inputs didn't trigger enough collisions on the most frequent words to affect the final output, effectively hiding a severe flaw.

### 8.
| Probing Strategy | Max Load ($\alpha$) | data/secret/6big.in | data/secret/7huge.in | Notes                            |
| :--------------- | :------------------ | :------------------ | :------------------- | :------------------------------- |
| **Linear**       | 0.3                 | *Timeout*           | *Timeout*            | Excessive resizing/reallocating  |
| **Linear**       | 0.5                 | 132 ms              | 550 ms               | Baseline performance             |
| **Linear**       | 0.75                | 182 ms              | 681 ms               | Noticeable slowdown (Clustering) |
| **Linear**       | 1.0                 | 256 ms              | 810 ms               | Significant performance hit      |
| **Quadratic**    | 0.5                 | 137 ms              | 547 ms               | Comparable to Linear             |
| **Quadratic**    | 0.75                | 142 ms              | 635 ms               | Faster than Linear at 0.75       |
| **Quadratic**    | 1.0                 | 147 ms              | 623 ms               | Highly stable performance*       |

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

### 0.5
data/secret/6big.in
Time elapsed: 132 ms
Correct!
data/secret/7huge.in
Time elapsed: 550 ms
Correct!

### 1.0
data/secret/6big.in
Time elapsed: 256 ms
Correct!
data/secret/7huge.in
Time elapsed: 810 ms
Correct!

### 0.3
I waited a few minutes, but 6big.in took way too long. Having such a low maximum causes a complete resizing/reallocation of the entire table over and over.

#### QuadraticProbing, no optimization

##### 0.75
data/secret/6big.in
Time elapsed: 142 ms
Correct!
data/secret/7huge.in
Time elapsed: 635 ms
Correct!

### 0.5
data/secret/6big.in
Time elapsed: 137 ms
Correct!
data/secret/7huge.in
Time elapsed: 547 ms
Correct!

### 1.0
data/secret/6big.in
Time elapsed: 147 ms
Correct!
data/secret/7huge.in
Time elapsed: 623 ms
Correct!
