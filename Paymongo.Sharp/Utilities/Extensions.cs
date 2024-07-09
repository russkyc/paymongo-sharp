// MIT License
// 
// Copyright (c) $CURRENT_YEAR$ Russell Camo (@russkyc)
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

using System;

namespace Paymongo.Sharp.Utilities
{
    public static class Extensions
    {
        public static int ToIntAmount(this decimal decimalValue)
        {
            int decimalPlaces = BitConverter.GetBytes(decimal.GetBits(decimalValue)[3])[2];
            decimal factor = (decimal)Math.Pow(10, decimalPlaces == 0 ? 2 : decimalPlaces);
            int intValue = (int)(decimalValue * factor);
            return intValue;
        }
        
        public static decimal ToDecimalAmount(this long intValue)
        {
            decimal factor = (decimal)Math.Pow(10, 2);
            decimal decimalValue = intValue / factor;
            return decimalValue;
        }
        
    }
}