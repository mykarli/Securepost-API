using System;
using System.Collections.Generic;

namespace FullStackBitirme.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;  // ✅ EKSİK OLAN KISIM
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // İlişki: 1 User -> N Post
        public List<Post> Posts { get; set; } = new();
    }
}
