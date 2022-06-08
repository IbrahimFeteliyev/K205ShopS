using Business.Abstract;
using Core.Entity.Models;
using Core.Security.Hasing;
using Core.Security.TokenHandler;
using DataAccess.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthManager
    {
        private readonly IAuthDal _authDal;
        private readonly HashingHandler _hashingHandler;
        public AuthManager(IAuthDal authDal, HashingHandler hashingHandler)
        {
            _authDal = authDal;
            _hashingHandler = hashingHandler;
        }

        public K205User Login(string email)
        {
            var user = _authDal.Get(x => x.Email == email);

            if (user == null)
                return null;

            return user;
        }

        public List<K205User> GetUsers()
        {
            return _authDal.GetAll();
        }

        public void Register(RegisterDTO model)
        {
            K205User user = new()
            {
                Email = model.Email,
                FullName = model.FullName,
                Password = _hashingHandler.PasswordHash(model.Password),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
            };
            _authDal.Add(user);
        }

        public K205User GetUserByEmail(string email)
        {
            return _authDal.Get(x => x.Email == email);
        }
    }
}
