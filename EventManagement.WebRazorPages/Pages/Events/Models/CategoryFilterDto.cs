﻿namespace EventManagement.WebRazorPages.Pages.Events.Models
{
    public class CategoryFilterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Selected { get; set; }
    }
}
