# trainline

## CSVReaderWriter coding task

### Trade-offs made
 * More time should have been spent on integration tests with mocking in place. Timebox nature of this task meant I spent to much time on refactorings and ensuring a performant design.
 * The design of the API was retained, however I feel there is still some repeated regions of the code.
 * No performance profilling was conducted. I would usually profile using the .NET profilling to ensure the design was performant.
 * I upgraded the .NET version of the project from .NET 4 to .NET 4.5 to use the Async features. I appreciate that in production environments this *might* not be possible.