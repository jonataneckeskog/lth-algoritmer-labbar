## Lab 2 - Hashtables

### 4.
The suitable number of list nodes is considered to be between 25% to 75% of the number of spaces avaliable in the array. Keeping it below 25% essentially wastes memory unnessesarily, while above 75% start to significantly slow down the performance.

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


