﻿namespace RecomendationSystemAPI.DTOs.Courses
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreditHours { get; set; }
        public List<string> Tags { get; set; }
    }
}
