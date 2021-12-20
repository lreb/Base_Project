using AutoMapper;
using BaseProject.API.Contract.DbContext;
using BaseProject.API.Domain.Models.Account;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BaseProject.API.Service.Users.Commands
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }

    public class CreateUserCommand : IRequest<int>, IMapFrom<User>
    {
        public int ResponsibleId { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Comments { get; set; }
        public DateTime Birthday { get; set; }
        //public string Aka { get; set; }
        //public bool IsOwner { get; set; }
        //public string AccountName { get; set; }

        public void Mapping(Profile profile)
        {
            var c = profile.CreateMap<User, CreateUserCommand>();
            //.ForMember(d => d.ResponsibleId, opt => opt.Ignore())
            //.ForMember(d => d.Comments, opt => opt.Ignore())
            //.ForMember(d => d.Aka, opt => opt.NullSubstitute("N/A"))
            //.ForMember(d => d.Birthday, opt => opt.MapFrom(s => s.BornDate));

            var d = profile.CreateMap<CreateUserCommand, User>()
                .ForMember(d => d.BornDate, opt => opt.MapFrom(s => s.Birthday))
                .ForMember(d=>d.Aka, opt=> opt.NullSubstitute("N/A"));
        }

        public class CreateProductCommandHandler : IRequestHandler<CreateUserCommand, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            public CreateProductCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<int> Handle(CreateUserCommand command, CancellationToken cancellationToken)
            {
                //TODO: add fluent validation

                var user = _mapper.Map<User>(command);
                _context.Users.Add(user);


                //TODO: validate if has new account


                //var account = _mapper.Map<Account>(command);

                //_context.Accounts.Add(account);


                //var rr = new AccountUser
                //{
                //    AccountId = account.Id,
                //    UserId = user.Id,
                //    IsOwner = command.IsOwner
                //};

                //TODO: add logger


                await _context.SaveChangesAsync($"{command.ResponsibleId}", default);
                return user.Id;
            }
        }
    }
}
