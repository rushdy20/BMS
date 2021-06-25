using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BMS.BusinessLayer;
using BMS_dotnet_WebApplication.Models.Shared;

namespace BMS_dotnet_WebApplication.Controllers
{
    
    public class FeedBackController : Controller
    {
        private readonly IFeedBack _feedBack;
        private readonly IMapper _mapper;

        public FeedBackController(IFeedBack feedBack,IMapper mapper)
        {
            _feedBack = feedBack;
            _mapper = mapper;

        }

        public async Task<ActionResult> Index()
        {
            var feedback = await _feedBack.ReadFeedBak();

            return View(feedback);
        }
    }
}
