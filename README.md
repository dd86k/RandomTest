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

Here is the result of a test over 1'000'000 iterations. (25th April 2016)

No optimization, do not prefer 32-bit.

At first, this is the default behavior.
```
Iterations: 1000000 | Size: 4000008 Bytes [0x003D0908]
One item is 0,0001%, lower values are better.
Making array... 46 Ticks [0x0000002E]
```

And this chart has been arranged in markdown.

Note that there is no point to pin-point flaws here.

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
