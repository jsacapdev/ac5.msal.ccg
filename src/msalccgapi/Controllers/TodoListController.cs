// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace msalccgapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ReadPolicy")]
    // [Authorize(Policy = "WritePolicy")] // this wont work because there is no mapping to the write policy
    public class TodoListController : ControllerBase
    {
        // In-memory TodoList
        private static readonly Dictionary<int, TodoItem> TodoStore = new Dictionary<int, TodoItem>();

        public TodoListController()
        {
            // Pre-populate with sample data
            if (TodoStore.Count == 0)
            {
                TodoStore.Add(1, new TodoItem() { Id = 1, Task = "Pick up groceries" });
                TodoStore.Add(2, new TodoItem() { Id = 2, Task = "Finish invoice report" });
                TodoStore.Add(3, new TodoItem() { Id = 3, Task = "Water plants" });
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            HttpContext.ValidateAppRole("Read");
            return Ok(TodoStore.Values);
        }
    }
}