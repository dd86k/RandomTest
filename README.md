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
Iterations: 1000000
One item is 0,000100%, lower values are better (Except for Common value)
Making array... 73 Ticks [0x0000000000000049]
```

And this chart has been arranged in markdown.

| Method | Time | Ticks | Common value | Times appearing | Pourcentage frequency |
| --- | --- | --- | --- | --- | --- |
| Local Random | 00:00:00.0147533 |    48986 t. | 137182 | x     9 | 0,000900% |
| New Random | 00:00:00.8060636 |  2676399 t. | 856469 | x 20429 | 2,042900% |
| Static Random | 00:00:00.0158899 |    52760 t. | 195636 | x     8 | 0,000800% |
| Local CryptoRandom | 00:00:00.2582853 |   857593 t. | 933920 | x     9 | 0,000900% |
| Static CryptoRandom | 00:00:00.2561075 |   850362 t. | 223548 | x     9 | 0,000900% |
| Local PCGRandom(0, 0) | 00:00:00.0730571 |   242574 t. |  48576 | x 10877 | 1,087700% |
| Local PCGRandom(inits) | 00:00:00.0722168 |   239784 t. | 483648 | x 10795 | 1,079500% |
| Static PCGRandom(0, 0) | 00:00:00.0711389 |   236205 t. |  48576 | x 10877 | 1,087700% |
| Static PCGRandom(inits) | 00:00:00.0708272 |   235170 t. | 483648 | x 10795 | 1,079500% |