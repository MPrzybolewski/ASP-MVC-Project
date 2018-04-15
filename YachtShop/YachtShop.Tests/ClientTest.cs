using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;
using YachtShop.Models;

namespace YachtShop.Tests
{
    public class ClientTest
    {
        private readonly ITestOutputHelper output;

        public ClientTest(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public void ValidModel_ShouldReturnTrue()
        {
            var model = new Client
            {
                FirstName = "Test",
                SecondName = "Test",
                PhoneNumber = "123-345-456",
                Email = "ytes@wp.pl"
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);
            if (!valid)
            {
                foreach (var validationResult in result)
                {
                    output.WriteLine(validationResult.ErrorMessage);
                }
            }
            Assert.True(valid);
        }

        [Fact]
        public void ModelWithPhoneNumberWithoutDash_ShouldReturnFalse()
        {
            var model = new Client
            {
                FirstName = "Test",
                SecondName = "Test",
                PhoneNumber = "123345456",
                Email = "ytes@wp.pl"
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);
            if (!valid)
            {
                foreach (var validationResult in result)
                {
                    output.WriteLine(validationResult.ErrorMessage);
                }
            }
            Assert.False(valid);
        }

        [Fact]
        public void ModelWithShorterPhoneNumber_ShouldReturnFalse()
        {
            var model = new Client
            {
                FirstName = "Test",
                SecondName = "Test",
                PhoneNumber = "123-345-45",
                Email = "ytes@wp.pl"
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);
          
            Assert.False(valid);
        }

        [Fact]
        public void ModelWithLongerPhoneNumber_ShouldReturnFalse()
        {
            var model = new Client
            {
                FirstName = "Test",
                SecondName = "Test",
                PhoneNumber = "123-345-4566",
                Email = "ytes@wp.pl"
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);

            Assert.False(valid);
        }


        [Fact]
        public void ModelWithEmailWithoutAt_ShouldReturnFalse()
        {
            var model = new Client
            {
                FirstName = "Test",
                SecondName = "Test",
                PhoneNumber = "123-345-456",
                Email = "yteswp.pl"
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);

            Assert.False(valid);
        }

        [Fact]
        public void ModelWithEmailWithoutCharactersAfterAt_ShouldReturnFalse()
        {
            var model = new Client
            {
                FirstName = "Test",
                SecondName = "Test",
                PhoneNumber = "123-345-456",
                Email = "ytes@"
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);

            Assert.False(valid);
        }

        [Fact]
        public void ModelWithEmailWithoutCharactersBeforeAt_ShouldReturnFalse()
        {
            var model = new Client
            {
                FirstName = "Test",
                SecondName = "Test",
                PhoneNumber = "123-345-456",
                Email = "@adasd.pl"
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);

            Assert.False(valid);
        }

        [Fact]
        public void ModelWithoutFirstName_ShouldReturnFalse()
        {
            var model = new Client
            {
                SecondName = "Test",
                PhoneNumber = "123-345-456",
                Email = "asdf@adasd.pl"
            };

            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);

            Assert.False(valid);
        }


    }
}
