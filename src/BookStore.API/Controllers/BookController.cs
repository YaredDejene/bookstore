
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Application.History;
using BookStore.Application.Interfaces;
using BookStore.Application.Models;
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
        public async Task<IEnumerable<BookModel>> Get()
        {
            return await _bookService.GetAll();
        }

        [HttpGet("{id:guid}")]
        public async Task<BookModel> Get(Guid id)
        {
            return await _bookService.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookModel bookModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _bookService.Register(bookModel));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] BookModel bookModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _bookService.Update(bookModel));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _bookService.Remove(id));
        }

        [HttpGet("history/{id:guid}")]
        public async Task<IEnumerable<BookHistoryData>> History(Guid id)
        {
            return await _bookService.GetAllHistory(id);
        }
    }
}