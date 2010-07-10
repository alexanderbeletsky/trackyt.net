﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Data.Linq;
using Trackyourtasks.Core.DAL;
using Trackyourtasks.Core.DAL.DataModel;
using Trackyourtasks.Core.Tests.Framework;
using Trackyourtasks.Core.DAL.Repositories.Impl;

namespace Trackyourtasks.Core.DAL.Tests
{
    [TestFixture]
    public class UsersRepositoryTests
    {
        [Test]
        public void Smoke()
        {
            var t = new UsersRepository();
        }

        [Test]
        public void InsertUser()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //INIT
                var register = new UsersRepository(fixture.Setup.Context);

                //ACT
                var user = new User()
                {
                    Email = "email",
                    SecretPhrase = "sec",
                    Password = "pass"
                };

                register.SaveUser(user);

                //POST
                var actual = register.FindUserByEmail("email");
                Assert.That(actual, Is.Not.Null);
            }
        }

        [Test]
        [ExpectedException(typeof(DuplicateKeyException))]
        public void InsertUserTwice()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //INIT
                var register = new UsersRepository(fixture.Setup.Context);

                //ACT / POST
                var user = new User()
                {
                    Email = "email",
                    SecretPhrase = "sec",
                    Password = "pass"
                };

                register.SaveUser(user);

                var newUser = new User()
                {
                    Email = "email",
                    SecretPhrase = "sec",
                    Password = "pass"
                };

                register.SaveUser(newUser);
            }
        }

        [Test]
        public void FindUserById()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //INIT
                var register = new UsersRepository(fixture.Setup.Context);

                var user = new User()
                {
                    Email = "email",
                    SecretPhrase = "sec",
                    Password = "pass"
                };

                register.SaveUser(user);

                //ACT
                var foundUser = register.FindUserById(user.Id);

                //POST
                Assert.That(foundUser, Is.Not.Null);
                Assert.That(foundUser.Id, Is.EqualTo(user.Id));
            }
        }

        [Test]
        public void UpdateUser()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //INIT
                var register = new UsersRepository(fixture.Setup.Context);

                var user = new User()
                {
                    Email = "email",
                    SecretPhrase = "sec",
                    Password = "pass"
                };

                register.SaveUser(user);

                //ACT
                user.SecretPhrase = "newsec";
                register.SaveUser(user);

                //POST
                var foundUser = register.FindUserById(user.Id);
                Assert.That(foundUser, Is.Not.Null);
                Assert.That(foundUser.SecretPhrase, Is.EqualTo("newsec"));
            }
        }

        [Test]
        public void DeleteUser()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //INIT
                var register = new UsersRepository(fixture.Setup.Context);

                var user = new User()
                {
                    Email = "email",
                    SecretPhrase = "sec",
                    Password = "pass"
                };

                register.SaveUser(user);

                //ACT
                register.DeleteUser(user);

                //POST
                var foundUser = register.FindUserById(user.Id);
                Assert.That(foundUser, Is.Null);
            }
        }
    }
}
