﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieStore.Models.Entities;
using MovieStore.Models.ViewModels;
using MovieStore.Repository.Abstract;
using NuGet.Protocol;

namespace MovieStore.Controllers
{
    public class DirectorController : Controller
    {
        private readonly IDirectorRepository _repository;
        private readonly IMapper _mapper;

        public DirectorController(IDirectorRepository directorRepository, IMapper mapper)
        {
            _repository = directorRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View(_repository.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DirectorVM newDirector)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(_mapper.Map<Director>(newDirector));
                TempData["success"] = "New director is added successfully";
                return RedirectToAction("index");
            }

            return View(newDirector);

        }

        public IActionResult Edit(int id)
        {
            return View(_mapper.Map<DirectorVM>(_repository.GetById(id)));
        }

        [HttpPost]
        public IActionResult Edit(DirectorVM updatedDirector)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(_mapper.Map<Director>(updatedDirector));
                TempData["success"] = "New director is updated successfully";
                return RedirectToAction("index");
            }
            return View(updatedDirector);
        }

        public IActionResult Delete(int id)
        {
            return View(_mapper.Map<DirectorVM>(_repository.GetById(id)));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            TempData["success"] = "New director is deleted successfully";
            return RedirectToAction("index");
        }

        public IActionResult Details(int id)
        {
            ViewBag.DirectedMovies = new SelectList(_repository.GetById(id).DirectedMovies, "Id", "Name");

            return View(_mapper.Map<DirectorVM>(_repository.GetById(id)));
        }

    }
}
