using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Assert =NUnit.Framework.Assert;

namespace Interview
{
    [TestFixture]
    public class Tests
    {
        private IRepository<TestStoreable> _repository;
        TestStoreable _firstRecord;
        TestStoreable _secondRecord;
        TestStoreable _thirdRecord;

        [SetUp]
        public void Initialize()
        {
            _repository = new Repository<TestStoreable>();
            _firstRecord = new TestStoreable { Id = 1, Name = "test1" };
            _secondRecord = new TestStoreable { Id = 2, Name = "test2" };
            _thirdRecord = new TestStoreable { Id = 3, Name = "test3" };
        }

        [Test]
        public void Testing_IRepository_GetAll_Returns_Expected_IEnumerable_Type()
        {
            var expected = _repository.All();
            Assert.IsInstanceOf<IEnumerable<TestStoreable>>(expected);
        }

        [Test]
        public void Testing_IRepository_Save_Method_Adds_New_Record()
        {
            _repository.Save(_firstRecord);

            var expected = _repository.All();
            Assert.IsTrue(expected.Count() == 1);
            Assert.IsTrue(expected.Contains(_firstRecord));
        }

        [Test]
        public void Test_IRepository_Save_Method_Not_Storing_Duplicate_Records()
        {
            _repository.Save(_firstRecord);
            _repository.Save(_secondRecord);
            _repository.Save(_thirdRecord);
            _repository.Save(_firstRecord);

            var expected = _repository.All();
            Assert.IsTrue(expected.Count() == 3);
            Assert.IsTrue(expected.Contains(_firstRecord));
            Assert.IsTrue(expected.Contains(_secondRecord));
            Assert.IsTrue(expected.Contains(_thirdRecord));
        }


        [Test]
        public void Testing_IRepository_FindById_Returns_Matching_Record()
        {
            _repository.Save(_firstRecord);
            _repository.Save(_secondRecord);

            var results = _repository.FindById(_firstRecord.Id);

            Assert.IsTrue(Equals(results.Id, _firstRecord.Id));
        }

        [Test]
        public void Testing_IRepository_FindById_Returns_Null_When_NO_Matching_Record_Found()
        {
            var results = _repository.FindById(_firstRecord.Id);

            Assert.IsNull(results);
        }

        [Test]
        public void Testing_IRepository_Delete_Removes_Expected_Record()
        {
            _repository.Save(_firstRecord);
            _repository.Save(_secondRecord);

             _repository.Delete(_firstRecord.Id);

             var results = _repository.FindById(_firstRecord.Id);

             Assert.IsNull(results);

        }

        [Test]
        public void Testing_IRepository_Delete_Donot_Throw_Exception_When_NO_Matching_Record_Found()
        {
            _repository.Save(_secondRecord);

            _repository.Delete(_firstRecord.Id);

             var expected = _repository.All();
             Assert.IsTrue(expected.Contains(_secondRecord));


        }

    }
}