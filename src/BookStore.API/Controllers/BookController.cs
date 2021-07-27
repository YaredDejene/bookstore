
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Application.History;
using BookStore.Application.Interfaces;
using BookStore.Application.Models;
using BookStore.Application.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    public class BookController : ApiController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _bookService.GetAll());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _bookService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookModel bookModel)
        {
            return Ok(await _bookService.Register(bookModel));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] BookModel bookModel)
        {
            return Ok(await _bookService.Update(bookModel));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _bookService.Remove(id));
        }

        [HttpPost]
        [Route("list")]
        public async Task<IActionResult> List([FromBody] DataTableRequest request)
        {
            if (request == null) return BadRequest();
            return Ok( await _bookService.GetAll(request));
        }

        [HttpPost]
        [Route("history")]
        public async Task<IActionResult> History([FromBody] DataTableRequest request)
        {
            if (request == null) return BadRequest();
            return Ok( await _bookService.GetAllHistory(request));
        }
    }
}