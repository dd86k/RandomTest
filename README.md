# RandomTest

This is a small set of tests to test the various ways to create pseudo-random values at any scale.

For fun.

Also because I was wondering if I could make my _Smug bots_ a bit more random.

This test may be flawed, even since I am not a statistician.

## How

This is how it goes down (tl;dr):
- Make an array (signed 32bit array).
- Fill it up with random values (go one by one).
  - Maximum bound is the number of iterations.
    - Usually `Array[Index] = r.Next(Iterations);`
- Retrieve statistics.
- Clear array and restart timer.

## Notes

- There are some static variables because I've once heard that it produces more random values in the long run. (How even? Who knows.)
- New Random spawns new values like this: `new Random().Next(Iterations)`
- CryptoRandom is an implementation derived from `System.Security.Cryptography.RandomNumberGenerator`
- PCGRandom is based on the [pcg-random](http://www.pcg-random.org/) family.

## Results

No optimization, do not prefer 32-bit.

### Small scale

26th April 2016

```
Iterations: 100 | Size: 408 Bytes [0x00000198]
One item is 1,00%, lower values are better.
Making array... 13 Ticks [0x0000000D]
```

| Local Random | 00:00:00.0000066 |       22 t. |     53 | x     4 | 4,00%
| New Random | 00:00:00.0000888 |      295 t. |     81 | x   100 | 100%
| Static Random | 00:00:00.0000027 |        9 t. |     53 | x     4 | 4,00%
| Local CryptoRandom | 00:00:00.0052783 |    17526 t. |     23 | x     7 | 7,00%
| Static CryptoRandom | 00:00:00.0000427 |      142 t. |     53 | x     3 | 3,00%
| Local PCGRandom(0, 0) | 00:00:00.0003764 |     1250 t. |     32 | x     7 | 7,00%
| Local PCGRandom(inits) | 00:00:00.0032487 |    10787 t. |     32 | x     8 | 8,00%
| Static PCGRandom(0, 0) | 00:00:00.0000057 |       19 t. |     32 | x     7 | 7,00%
| Static PCGRandom(inits) | 00:00:00.0000072 |       24 t. |      4 | x     8 | 8,00%

### Large scale

26th April 2016

```
Iterations: 1000000 | Size: 4000008 Bytes [0x003D0908]
One item is 0,0001%, lower values are better.
Making array... 46 Ticks [0x0000002E]
```

| Method | Time | Ticks | Common value | Times appearing | Pourcentage frequency |
| --- | --- | --- | --- | --- | --- |
| Local Random | 00:00:00.0143557 |    47666 t. | 636736 | x    10 | 0,0010%
| New Random | 00:00:00.8103200 |  2690534 t. | 685628 | x 20258 | 2,0258%
| Static Random | 00:00:00.0148756 |    49392 t. | 346275 | x     9 | 0,0009%
| Local CryptoRandom | 00:00:00.2521278 |   837149 t. |  92896 | x     8 | 0,0008%
| Static CryptoRandom | 00:00:00.2405163 |   798595 t. | 874405 | x     9 | 0,0009%
| Local PCGRandom(0, 0) | 00:00:00.0716906 |   238037 t. |  48576 | x 10877 | 1,0877%
| Local PCGRandom(inits) | 00:00:00.0744442 |   247180 t. | 870912 | x 10774 | 1,0774%
| Static PCGRandom(0, 0) | 00:00:00.0709786 |   235673 t. |  48576 | x 10877 | 1,0877%
| Static PCGRandom(inits) | 00:00:00.0712994 |   236738 t. | 217728 | x 10903 | 1,0903%