# trainline

## CSVReaderWriter coding task

### Trade-offs made
 * More time should have been spent on integration tests with mocking in place. Time-box nature of this task meant I spent too much time on refactoring and ensuring a performant design.
 * The design of the API was retained; however I feel there is still some repeated regions of the code.
 * No performance profiling was conducted. I would usually profile using the .NET profiling to ensure the design was performant.
 * I upgraded the .NET version of the project from .NET 4 to .NET 4.5 to use the Async features. I appreciate that in production environments this *might* not be possible.
