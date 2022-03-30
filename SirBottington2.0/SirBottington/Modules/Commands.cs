using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using SirBottington.Models;
using SirBottington.Services.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirBottington.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        private readonly IConfiguration _configuration;
        private readonly IUserDataAccess _db;
        private readonly Random _r;
        private List<SocketUser> usersOnCooldown;

        public Commands(IConfiguration configuration, IUserDataAccess db, Random r)
        {
            _configuration = configuration;
            _db = db;
            _r = r;
            usersOnCooldown = new List<SocketUser>();
        }

        [Command("vibes")]
        [Summary("")]
        public async Task VibesAsync()
        {
            await ReplyAsync(_configuration["Vibes"]);
        }

        [Command("shoot")]
        public async Task ShootAsync(SocketUser target)
        {
            if (target.IsBot)
            {
                await ReplyAsync("You can't target bots");
                return;
            }
            else if (target.Status != Discord.UserStatus.Online)
            {
                await ReplyAsync("You can only target online people");
                return;
            }
            else
            {
                var userProfile = await _db.GetUser(Context.User.Discriminator);
                if (userProfile.Bullets <= 0)
                {
                    await ReplyAsync("You have no bullets, go reload then comeback");
                    return;
                }
                var rng = _r.NextSingle();
                if (rng < 0.7)
                {
                    Console.WriteLine("Rolled " + rng + " to hit");
                    var targetProfile = await _db.GetUser(target.Discriminator);
                    if (targetProfile is null)
                    {
                        targetProfile = new UserModel { Username = target.Username, Bullets = 0, Discriminator = target.Discriminator };
                        await _db.Insert(targetProfile);
                    }

                    // hit
                    // You get points equal to how many bullets you remove from the enemy player. 
                    // If you haven't hit anyone yet, aka your hits is null. you get equal to enemy bullets, otherwise it adds to your total hits.
                    userProfile.Hits = userProfile.Hits is not null ? userProfile.Hits + targetProfile.Bullets : targetProfile.Bullets;


                    targetProfile.Bullets = 0;

                    await ReplyAsync(Context.User + " hit");
                    var targetDmChannel = await target.CreateDMChannelAsync();
                    await targetDmChannel.SendMessageAsync(Context.User + " hit you with a gunshot. You lost all your bullets. Get loading to take your revenge");
                    await _db.Update(targetProfile);

                }
                else
                {
                    // miss
                    await ReplyAsync(Context.User + " missed");
                }

                userProfile.Bullets--;
                await _db.Update(userProfile);
            }



        }

        [Command("score")]
        public async Task ScoreAsync()
        {
            var users = await _db.GetAll();
            users = users.Where(x => x.Hits != null).ToList();
            users = users.OrderByDescending(x => x.Hits).ToList();

            var contextGuild = Context.Guild;
            //await contextGuild.DownloadUsersAsync();
            var guildUsers = contextGuild.Users;

            //users = users.IntersectBy(guildUsers, x=> x.Discriminator).OrderByDescending(x=> x.Hits).ToList();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("```");
            sb.AppendLine("Score for Bullet Game");
            foreach (var user in users)
            {
                sb.Append(user.Username);
                sb.Append(": ");
                sb.Append(user.Hits);
                sb.AppendLine(" points");
            }
            sb.Append("```");
            await ReplyAsync(sb.ToString());
        }

        [Command("reload")]
        public async Task ReloadAsync()
        {
            if (await CheckOnCooldown(Context.User) == false)
            {
                // get user profile
                var userProfile = await _db.GetUser(Context.User.Discriminator);
                // get one bullet. 
                if (userProfile is not null)
                {
                    if (userProfile.Bullets is not null)
                    {
                        userProfile.Bullets++;
                    }
                    else
                    {
                        userProfile.Bullets = 1;
                    }
                    await _db.Update(userProfile);
                }
                else
                {
                    userProfile = new UserModel { Username = Context.User.Username, Discriminator = Context.User.Discriminator, Bullets = 1 };
                    await _db.Insert(userProfile);
                }
                await ReplyAsync("You load your gun " + Context.User);
                await AddToCooldown(Context.User);

            }
            else
            {
                await ReplyAsync("You're on cooldown");
            }
        }

        private async Task<bool> CheckOnCooldown(SocketUser user)
        {
            return usersOnCooldown.Contains(user);
        }
        private async Task AddToCooldown(SocketUser user)
        {
            usersOnCooldown.Add(user);
            Thread.Sleep(600000);
            var dmChannel = await user.CreateDMChannelAsync();
            await dmChannel.SendMessageAsync("You can load your gun again.");
            usersOnCooldown.Remove(user);
        }
    }
}
