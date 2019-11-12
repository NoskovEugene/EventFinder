using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EventFinder.Models.Repositories;
using EventFinder.Models.Entity;

namespace EventFinder.Controllers
{
    [Authorize()]
    public class Setting : Controller
    {
        public IRepositoryBase<User> UserRepository {get;}

        public Setting(IRepositoryBase<User> userRepository)
        {
            this.UserRepository = userRepository;
        }


        public IActionResult Settings(int? id)
        {
            if(id == null){
                return BadRequest();
            }
            else{
                Models.Entity.User user = UserRepository.Find((int)id);
                return View(user);
            }
        }

    }
}