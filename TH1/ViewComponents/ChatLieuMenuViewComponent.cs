using Microsoft.AspNetCore.Mvc;
using TH1.Models;
using TH1.Repository;

namespace TH1.ViewComponents
{
    public class ChatLieuMenuViewComponent : ViewComponent
    {
        private readonly IChatLieuSpRepository _ChatLieu;
        public ChatLieuMenuViewComponent(IChatLieuSpRepository ChatLieu)
        {
            _ChatLieu = ChatLieu;
        }
        public IViewComponentResult Invoke()
        {
            var ds = _ChatLieu.GetAllChatLieus().OrderBy(x=>x.MaChatLieu);
            return View(ds);
        }
    }
}