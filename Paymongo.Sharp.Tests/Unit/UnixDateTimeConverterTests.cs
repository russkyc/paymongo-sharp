// MIT License
// 
// Copyright (c) 2025 Russell Camo (@russkyc)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Text.Json;

namespace Paymongo.Sharp.Tests.Unit;

public class UnixDateTimeConverterTests
{
    [Theory]
    [InlineData(1757980800, 2025, 09, 16, 8, 0, 0)]
    [InlineData(0, 1970, 1, 1, 8, 0, 0)]
    Task ConvertUnixTimestampToDateTime(long unixTimestamp, int year, int month, int day, int hour, int minute, int second)
    {
        // Arrange
        var converter = new Converters.UnixDateTimeConverter();
        var reader = new Utf8JsonReader(System.Text.Encoding.UTF8.GetBytes(unixTimestamp.ToString()));

        // Act
        reader.Read(); // Move to the number token
        var result = converter.Read(ref reader, typeof(DateTime), new JsonSerializerOptions());

        // Assert
        result.Should().Be(new DateTime(year, month, day, hour, minute, second, DateTimeKind.Local));
        return Task.CompletedTask;
    }
}