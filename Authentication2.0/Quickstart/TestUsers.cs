// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer4.Quickstart.UI
{
    public class TestUsers
    {
        public static List<TestUser> Users = new List<TestUser>
        {
            new TestUser {
                SubjectId = "818727",
                Username = "alice",
                Password = "alice", 
                Claims = 
                {
                    new Claim(JwtClaimTypes.Name, "Alice Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Alice"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 
                                                         'locality': 'Heidelberg', 
                                                         'postal_code': 69118, 
                                                         'country': 'Germany' }", 
                        IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                }
            },
            new TestUser {
                SubjectId = "88421113",
                Username = "ethan",
                Password = "ethan", 
                Claims = 
                {
                    new Claim(JwtClaimTypes.Name, "Ethan Easterling"),
                    new Claim(JwtClaimTypes.GivenName, "Ethan"),
                    new Claim(JwtClaimTypes.FamilyName, "Easterling"),
                    new Claim(JwtClaimTypes.Email, "ethaneasterling@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://ethan.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 
                                                         'locality': 'Heidelberg', 
                                                         'postal_code': 69118, 
                                                         'country': 'Germany' }", 
                        IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                    new Claim("location", "somewhere", ClaimValueTypes.String)
                }
            }
        };
    }
}
