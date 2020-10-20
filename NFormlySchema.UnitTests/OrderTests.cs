using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace NFormlySchema.UnitTests
{
    public class OrderTests
    {
        [Fact]
        public void PlainType()
        {
            var fieldConfigCollection = FormlySchema.FromType<OrderFoo>();
            fieldConfigCollection.Select(x => x.Key).Should().BeInAscendingOrder();
        }

        [Fact]
        public void TypeWithNested()
        {
            var fieldConfigCollection = FormlySchema.FromType<OrderFooWithNested>();
            fieldConfigCollection.Select(x => x.Key).Should().BeInAscendingOrder();
        }
    }

    public class OrderFoo
    {
        // Order == 10_000 is default
        public string A2 { get; set; }

        [Display(Order = 10_001)]
        public string A3 { get; set; }

        [Display(Order = 9999)]
        public string A1 { get; set; }
    }

    public class OrderFooWithNested
    {
        [Display(Order = 20)]
        public string A20 { get; set; }

        [Display(Order = 30)]
        public OrderFooChild A30 { get; set; }

        [Display(Order = 10)]
        public string A10 { get; set; }
    }

    public class OrderFooChild
    {
        [Display(Order = 2)]
        public string A32 { get; set; }

        [Display(Order = 1)]
        public string A31 { get; set; }
    }
}