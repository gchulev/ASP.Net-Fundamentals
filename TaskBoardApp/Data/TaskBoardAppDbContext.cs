using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using TaskBoardApp.Data.Models;

using Task = TaskBoardApp.Data.Models.Task;

namespace TaskBoardApp.Data
{
	public class TaskBoardAppDbContext : IdentityDbContext
	{
		public TaskBoardAppDbContext(DbContextOptions<TaskBoardAppDbContext> options)
			: base(options)
		{
			this.Database.Migrate();
		}

		public DbSet<Task> Tasks { get; set; } = null!;
		public DbSet<Board> Boards { get; set; } = null!;

		private IdentityUser TestUser { get; set; } = null!;
		private Board OpenBoard { get; set; } = null!;
		private Board InProgressBoard { get; set; } = null!;
		private Board DoneBoard { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Task>()
				.HasOne(t => t.Board)
				.WithMany(b => b.Tasks)
				.HasForeignKey(t => t.BoardId)
				.OnDelete(DeleteBehavior.Restrict);

			SeedUsers();
			modelBuilder
				.Entity<IdentityUser>()
				.HasData(this.TestUser);

			SeedBoards();
			modelBuilder
				.Entity<Board>()
				.HasData(this.OpenBoard, this.InProgressBoard, this.DoneBoard);

			modelBuilder.Entity<Task>()
				.HasData(new Task()
				{
					Id = 1,
					Title = "Improve CSS Styles",
					Description = "Implement better styling for all public pages",
					CreatedOn = DateTime.Now.AddDays(-200),
					OwnerId = this.TestUser.Id,
					BoardId = this.OpenBoard.Id
					
				},
				new Task()
				{
					Id = 2,
					Title = "Android Client App",
					Description = "Create Android Client App for the TaskBoard RESTful API",
					CreatedOn = DateTime.Now.AddDays(-5),
					OwnerId = this.TestUser.Id,
					BoardId = this.OpenBoard.Id
				},
				new Task()
				{
					Id = 3,
					Title = "Desktop Client App",
					Description = "Create Windows Forms desktop app client for the TaskBoard RESTfull API",
					CreatedOn = DateTime.Now.AddDays(-1),
					OwnerId = this.TestUser.Id,
					BoardId = this.InProgressBoard.Id
				},
				new Task()
				{
					Id = 4,
					Title = "Create Tasks",
					Description = "Implement [Create Task] page for adding new tasks",
					CreatedOn = DateTime.Now.AddDays(-1),
					OwnerId = this.TestUser.Id,
					BoardId = this.DoneBoard.Id
				});

			base.OnModelCreating(modelBuilder);
		}

		private void SeedBoards()
		{
			this.OpenBoard = new Board()
			{
				Id = 1,
				Name = "Open"
			};

			this.InProgressBoard = new Board()
			{
				Id = 2,
				Name = "In Progress"
			};

			this.DoneBoard = new Board()
			{
				Id = 3,
				Name = "Done"
			};
		}

		private void SeedUsers()
		{
			var passwordHasher = new PasswordHasher<IdentityUser>();

			this.TestUser = new IdentityUser()
			{
				UserName = "test@softuni.bg",
				NormalizedUserName = "TEST@SOFTUNI.BG"
			};

			this.TestUser.PasswordHash = passwordHasher.HashPassword(this.TestUser, "softuni");
		}
	}
}