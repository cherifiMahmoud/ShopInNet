﻿using AutoMapper;
using Domain.IRepository;
using Domain.Models;
using Dto.Categorie;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers.CategoorieController
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(400)]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategorieRepository categorieRepository;

        public CategoriesController(ICategorieRepository categorieRepository, IMapper mapper)
        {
            this.categorieRepository = categorieRepository;
            Mapper = mapper;
        }

        public IMapper Mapper { get; }

        [HttpGet]
        [ProducesResponseType(200,Type =typeof(List<CategorieGetDTO>))]
        [ProducesDefaultResponseType]
        public IActionResult GetCategories()
        {
            var CategorieList = categorieRepository.GetCategories();
            var CategorieDTOList = new List<CategorieGetDTO>();

            foreach (var Obj in CategorieList)
            {
                CategorieDTOList.Add(Mapper.Map<CategorieGetDTO>(Obj));
            }
            return Ok(CategorieDTOList);
        }

        [HttpGet("{CategorieId}", Name = "GetCategorie")]
        [ProducesResponseType(200, Type = typeof(CategorieGetDTO))]
        [ProducesDefaultResponseType]
        public IActionResult GetCategorie(int CategorieId)
        {
            var CategorieObj = categorieRepository.GetCategorie(CategorieId);

            if ( CategorieObj == null)
            {
                return NotFound();
            }

            var CategorieDTO = Mapper.Map<CategorieGetDTO>(CategorieObj);
            return Ok(CategorieDTO);
        }

        [HttpGet("Search/{CategorieName}")]
        [ProducesResponseType(200, Type = typeof(CategorieGetDTO))]
        [ProducesDefaultResponseType]
        public IActionResult GetCategorie(string CategorieName)
        {
            var CategorieObj = categorieRepository.GetCategorie(CategorieName);

            if (CategorieObj == null)
            {
                return NotFound();
            }

            var CategorieDTO = Mapper.Map<CategorieGetDTO>(CategorieObj);
            return Ok(CategorieDTO);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CategorieInsertDTO))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult CreateCategorie([FromBody] CategorieInsertDTO categorieInsertDTO )
        {
            if (categorieInsertDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (categorieRepository.CheckCategorieExist(categorieInsertDTO.CategorieName))
            {
                ModelState.AddModelError(string.Empty, "Categorie Exists");
                return StatusCode(404, ModelState);
            }

            var CategorieObj = Mapper.Map<Categorie>(categorieInsertDTO);
            if (! categorieRepository.CreateCategorie(CategorieObj))
            {
                ModelState.AddModelError(string.Empty, $"Somthing went rong when adding record {CategorieObj.CategorieName}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCategorie", new { CategorieId = CategorieObj.CategorieId }, categorieInsertDTO);
        }

        [HttpPatch("{CategorieId}")]
        [ProducesResponseType(200, Type = typeof(CategorieUpdateDTO))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateCategorie(int CategorieId, [FromBody]CategorieUpdateDTO categorieUpdateDTO)
        {
            if (categorieUpdateDTO == null || CategorieId != categorieUpdateDTO.CategorieId)
            {
                return BadRequest(ModelState);
            }

            var CategorieObjName = categorieRepository.GetCategorie(categorieUpdateDTO.CategorieName);
            if (CategorieObjName != null)
            {
                if (CategorieObjName.CategorieId !=  CategorieId)
                {
                    ModelState.AddModelError(string.Empty, "Categorie Exists");
                    return StatusCode(404, ModelState);
                }
            }

            var CategorieObj = Mapper.Map<Categorie>(categorieUpdateDTO);
            if (!categorieRepository.UpdateCategorie(CategorieObj))
            {
                ModelState.AddModelError(string.Empty, $"Somthing went rong when Updating record {CategorieObj.CategorieName}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCategorie", new { CategorieId = CategorieObj.CategorieId }, categorieUpdateDTO);
        }

        [HttpDelete("{CategorieId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(409)]
        public IActionResult DeleteCategorie(int CategorieId)
        {
            if (! categorieRepository.CheckCategorieExist(CategorieId))
            {
                return NotFound();
            }

            var CategorieObj = categorieRepository.GetCategorie(CategorieId);

            if (!categorieRepository.DeleteCategorie(CategorieObj))
            {
                ModelState.AddModelError(string.Empty, $"Somthing went rong when Deleting record {CategorieObj.CategorieName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
