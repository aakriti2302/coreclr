// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/******************************************************************************
 * This file is auto-generated from a template file by the GenerateTests.csx  *
 * script in tests\src\JIT\HardwareIntrinsics\General\Shared. In order to make    *
 * changes, please update the corresponding template and run according to the *
 * directions listed in the file.                                             *
 ******************************************************************************/

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace JIT.HardwareIntrinsics.General
{
    public static partial class Program
    {
        private static void GetAndWithLowerAndUpperSingle()
        {
            var test = new VectorGetAndWithLowerAndUpper__GetAndWithLowerAndUpperSingle();

            // Validates basic functionality works
            test.RunBasicScenario();

            // Validates calling via reflection works
            test.RunReflectionScenario();

            if (!test.Succeeded)
            {
                throw new Exception("One or more scenarios did not complete as expected.");
            }
        }
    }

    public sealed unsafe class VectorGetAndWithLowerAndUpper__GetAndWithLowerAndUpperSingle
    {
        private static readonly int LargestVectorSize = 32;

        private static readonly int ElementCount = Unsafe.SizeOf<Vector256<Single>>() / sizeof(Single);

        public bool Succeeded { get; set; } = true;

        public void RunBasicScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunBasicScenario));

            Single[] values = new Single[ElementCount];

            for (int i = 0; i < ElementCount; i++)
            {
                values[i] = TestLibrary.Generator.GetSingle();
            }

            Vector256<Single> value = Vector256.Create(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7]);

            Vector128<Single> lowerResult = value.GetLower();
            Vector128<Single> upperResult = value.GetUpper();
            ValidateGetResult(lowerResult, upperResult, values);

            Vector256<Single> result = value.WithLower(upperResult);
            result = result.WithUpper(lowerResult);
            ValidateWithResult(result, values);
        }

        public void RunReflectionScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunReflectionScenario));

            Single[] values = new Single[ElementCount];

            for (int i = 0; i < ElementCount; i++)
            {
                values[i] = TestLibrary.Generator.GetSingle();
            }

            Vector256<Single> value = Vector256.Create(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7]);

            object lowerResult = typeof(Vector256<Single>)
                                    .GetMethod(nameof(Vector256<Single>.GetLower), new Type[] { })
                                    .Invoke(value, new object[] { });
            object upperResult = typeof(Vector256<Single>)
                                    .GetMethod(nameof(Vector256<Single>.GetUpper), new Type[] { })
                                    .Invoke(value, new object[] { });
            ValidateGetResult((Vector128<Single>)(lowerResult), (Vector128<Single>)(upperResult), values);

            object result = typeof(Vector256<Single>)
                                .GetMethod(nameof(Vector256<Single>.WithLower), new Type[] { typeof(Vector128<Single>) })
                                .Invoke(value, new object[] { upperResult });
            result = typeof(Vector256<Single>)
                        .GetMethod(nameof(Vector256<Single>.WithUpper), new Type[] { typeof(Vector128<Single>) })
                        .Invoke(result, new object[] { lowerResult });
            ValidateWithResult((Vector256<Single>)(result), values);
        }

        private void ValidateGetResult(Vector128<Single> lowerResult, Vector128<Single> upperResult, Single[] values, [CallerMemberName] string method = "")
        {
            Single[] lowerElements = new Single[ElementCount / 2];
            Unsafe.WriteUnaligned(ref Unsafe.As<Single, byte>(ref lowerElements[0]), lowerResult);

            Single[] upperElements = new Single[ElementCount / 2];
            Unsafe.WriteUnaligned(ref Unsafe.As<Single, byte>(ref upperElements[0]), upperResult);

            ValidateGetResult(lowerElements, upperElements, values, method);
        }

        private void ValidateGetResult(Single[] lowerResult, Single[] upperResult, Single[] values, [CallerMemberName] string method = "")
        {
            for (int i = 0; i < ElementCount / 2; i++)
            {
                if (lowerResult[i] != values[i])
                {
                    Succeeded = false;
                    break;
                }
            }

            if (!Succeeded)
            {
                TestLibrary.TestFramework.LogInformation($"Vector256<Single>.GetLower(): {method} failed:");
                TestLibrary.TestFramework.LogInformation($"   value: ({string.Join(", ", values)})");
                TestLibrary.TestFramework.LogInformation($"  result: ({string.Join(", ", lowerResult)})");
                TestLibrary.TestFramework.LogInformation(string.Empty);
            }

            for (int i = ElementCount / 2; i < ElementCount; i++)
            {
                if (upperResult[i - (ElementCount / 2)] != values[i])
                {
                    Succeeded = false;
                    break;
                }
            }

            if (!Succeeded)
            {
                TestLibrary.TestFramework.LogInformation($"Vector256<Single>.GetUpper(): {method} failed:");
                TestLibrary.TestFramework.LogInformation($"   value: ({string.Join(", ", values)})");
                TestLibrary.TestFramework.LogInformation($"  result: ({string.Join(", ", upperResult)})");
                TestLibrary.TestFramework.LogInformation(string.Empty);
            }
        }

        private void ValidateWithResult(Vector256<Single> result, Single[] values, [CallerMemberName] string method = "")
        {
            Single[] resultElements = new Single[ElementCount];
            Unsafe.WriteUnaligned(ref Unsafe.As<Single, byte>(ref resultElements[0]), result);
            ValidateWithResult(resultElements, values, method);
        }

        private void ValidateWithResult(Single[] result, Single[] values, [CallerMemberName] string method = "")
        {
            for (int i = 0; i < ElementCount / 2; i++)
            {
                if (result[i] != values[i + (ElementCount / 2)])
                {
                    Succeeded = false;
                    break;
                }
            }

            if (!Succeeded)
            {
                TestLibrary.TestFramework.LogInformation($"Vector256<Single.WithLower(): {method} failed:");
                TestLibrary.TestFramework.LogInformation($"   value: ({string.Join(", ", values)})");
                TestLibrary.TestFramework.LogInformation($"  result: ({string.Join(", ", result)})");
                TestLibrary.TestFramework.LogInformation(string.Empty);
            }

            for (int i = ElementCount / 2; i < ElementCount; i++)
            {
                if (result[i] != values[i - (ElementCount / 2)])
                {
                    Succeeded = false;
                    break;
                }
            }

            if (!Succeeded)
            {
                TestLibrary.TestFramework.LogInformation($"Vector256<Single.WithUpper(): {method} failed:");
                TestLibrary.TestFramework.LogInformation($"   value: ({string.Join(", ", values)})");
                TestLibrary.TestFramework.LogInformation($"  result: ({string.Join(", ", result)})");
                TestLibrary.TestFramework.LogInformation(string.Empty);
            }
        }
    }
}
