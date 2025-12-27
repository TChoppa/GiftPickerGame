using Microsoft.AspNetCore.Mvc;
using SantaGift.DBContext;
using SantaGift.Models;

namespace SantaGift.Controllers
{
    public class DashboardController : Controller
    {
        private readonly RegisterDb _dbContext;
        public DashboardController(RegisterDb db)
        {
            _dbContext = db;
        }

        public IActionResult DashBoard()
        {
            var userName = HttpContext.Session.GetString("UserName");
            var listParticipants = _dbContext.Participants.Where(x => x.UseName == userName).ToList();
            return View(listParticipants);
        }

        [HttpGet]
        public JsonResult GetParticipants()
        {
            try
            {
                var userName = HttpContext.Session.GetString("UserName");             
                var listParticipants = _dbContext.Participants
                    .Where(x => x.UseName == userName)
                    .Select(x => new { id = x.Id, name = x.Name })
                    .ToList();
                if(listParticipants!=null)
                {
                    return Json(listParticipants);
                }
                else
                {
                    return Json(new { success = false, message = "No Participants Found" });
                }
                
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public JsonResult GetParticipantsView()
        {
            try
            {
                var userName = HttpContext.Session.GetString("UserName");
                var listParticipants = _dbContext.ParticipantsView
                    .Where(x => x.UseName == userName)
                    .Select(x => new { id = x.Id, name = x.Name })
                    .ToList();
                if (listParticipants != null)
                {
                    return Json(listParticipants);
                }
                else
                {
                    return Json(new { success = false, message = "No Participants Found" });
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetGiftSponsers()
        {
            try
            {
                var userName = HttpContext.Session.GetString("UserName");
                var listSponsers = _dbContext.GiftSponsers
                    .Where(x => x.UseName == userName)
                    .Select(x => new { id = x.Id, sponser = x.Sponser , picker = x.Picker })
                    .ToList();
                if (listSponsers != null)
                {
                    return Json(listSponsers);
                }
                else
                {
                    return Json(new { success = false, message = "No Participants Found" });
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public IActionResult AddSponsers(string sponser , string picker)
        {
            if (sponser != null && picker!=null)
            {
                var userName = HttpContext.Session.GetString("UserName");

                var checkParticpant = _dbContext.GiftSponsers.Where(x => x.Sponser == sponser && x.UseName == userName && x.Picker==picker).FirstOrDefault();
                          if (checkParticpant == null)
                {

                    GiftSponsers giftSponser = new GiftSponsers();
                    giftSponser.Sponser = sponser;
                    giftSponser.Picker = picker;                
                    giftSponser.UseName = userName;
                    giftSponser.isParticipant = true;
                    _dbContext.GiftSponsers.Add(giftSponser);
                    _dbContext.SaveChanges();

                    return Json(new { success = true, message = "Participant added successfully" });
                }
                else
                {
                    return Json(new { success = false, message = "Participant already exists" });
                }

            }
            return Json(new { success = false, message = "Invalid participant name" });

        }

        [HttpPost]
        public JsonResult EditParticipant(int id, string name)
        {
            var participant = _dbContext.Participants.FirstOrDefault(x => x.Id == id);
            if (participant == null)
            {
                return Json(new { success = false, message = "Participant not found" });
            }

            participant.Name = name;
            _dbContext.SaveChanges();

            return Json(new { success = true, message = "Participant updated successfully" });
        }
        [HttpPost]
        public JsonResult EditParticipantView(string name , string oldName)
        {
            var userName = HttpContext.Session.GetString("UserName");
            var participant = _dbContext.ParticipantsView.FirstOrDefault(x => x.Name == oldName && x.UseName== userName);
            if (participant == null)
            {
                return Json(new { success = false, message = "Participant not found" });
            }

            participant.Name = name;
            _dbContext.SaveChanges();

            return Json(new { success = true, message = "Participant updated successfully" });
        }

        [HttpPost]
        public IActionResult AddParticipant(string name)
        {
            if(name!=null)
            {
                var userName = HttpContext.Session.GetString("UserName");

                var checkParticpant = _dbContext.Participants.Where(x => x.Name == name && x.UseName ==  userName).FirstOrDefault();
                var checkParticpantView = _dbContext.ParticipantsView.Where(x => x.Name == name && x.UseName == userName).FirstOrDefault();
                if (checkParticpant==null && checkParticpantView==null)
                {
                   
                    Participants participant = new Participants();
                    participant.Name = name;
                    participant.UseName = userName;

                    ParticipantsView participantView = new ParticipantsView();
                    participantView.Name = name;
                    participantView.UseName = userName;
                    participantView.isParticipant = true;

                    _dbContext.Participants.Add(participant);
                    _dbContext.ParticipantsView.Add(participantView);
                    _dbContext.SaveChanges();

                    return Json(new { success = true, message = "Participant added successfully" });
                }
             else
                    {
                        return Json(new { success = false, message = "Participant already exists" });
                    }
                
            }
            return Json(new { success = false, message = "Invalid participant name" });
            
        }

        [HttpPost]
        public IActionResult DeleteParticipant(int Id)
        {
            var isDelete = _dbContext.Participants.Where(x => x.Id == Id).FirstOrDefault();
            if(isDelete != null)
            {
                _dbContext.Participants.Remove(isDelete);
                _dbContext.SaveChanges();
                return Json(new { success = true, message = "Participant Deleted successfully" });
            }
            else
            {
                return Json(new { success = true, message = "Participant Was Not Deleted" });
            }
               

        }
        [HttpPost]
        public IActionResult DeleteParticipantViews(string name)
        {
            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
            {
                return Json(new { success = false, message = "Session expired. Please login again." });
            }
            var isDelete = _dbContext.ParticipantsView.Where(x => x.Name.Trim() == name && x.UseName == userName).FirstOrDefault();
            var isDeleteSponser = _dbContext.GiftSponsers.Where(s => s.Sponser == name && s.UseName == userName).FirstOrDefault();
            if (isDelete != null)
            {
                _dbContext.ParticipantsView.Remove(isDelete);
                _dbContext.SaveChanges();
                return Json(new { success = true, message = "Participant Deleted successfully" });
            }
            else if (isDeleteSponser != null)
            {
                _dbContext.GiftSponsers.Remove(isDeleteSponser);
                _dbContext.SaveChanges();
                return Json(new { success = true, message = "Participant Deleted successfully" });
            }
            else
            {
                return Json(new { success = true, message = "Participant Was Not Deleted" });
            }


        }
        [HttpPost]
        public IActionResult DeleteParticipantView(int Id)
        {
            var isDelete = _dbContext.ParticipantsView.Where(x => x.Id == Id).FirstOrDefault();
            if (isDelete != null)
            {
                _dbContext.ParticipantsView.Remove(isDelete);
                _dbContext.SaveChanges();
                return Json(new { success = true, message = "Participant Deleted successfully" });
            }
            else
            {
                return Json(new { success = true, message = "Participant Was Not Deleted" });
            }


        }
        [HttpPost]
        public IActionResult DeleteSponser(int Id)
        {
            var isDelete = _dbContext.GiftSponsers.Where(x => x.Id == Id).FirstOrDefault();
            if (isDelete != null)
            {
                _dbContext.GiftSponsers.Remove(isDelete);
                _dbContext.SaveChanges();
                return Json(new { success = true, message = "Participant Deleted successfully" });
            }
            else
            {
                return Json(new { success = true, message = "Participant Was Not Deleted" });
            }


        }
    }
}
