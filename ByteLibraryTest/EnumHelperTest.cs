using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ByteLibrary;
using Xunit;

namespace ByteLibraryTest
{
    public class EnumHelperTest
    {
        [Fact]
        public void GetValues_enumerates_all_possible_enum_values()
        {
            IEnumerable<TestEnum> enumValues = EnumHelper.GetValues<TestEnum>();
            Assert.True(enumValues.All(value => Enum.IsDefined(typeof(TestEnum), value)));
        }
    }
}
