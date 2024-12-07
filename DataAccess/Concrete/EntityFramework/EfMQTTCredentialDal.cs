﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework;

public class EfMQTTCredentialDal : EfEntityRepositoryBase<MQTTCredential, EfContext>, IMQTTCredentialDal
{
}
