// Global using directives

global using AutoFixture;
global using DotNet.Testcontainers.Builders;
global using DotNet.Testcontainers.Configurations;
global using DotNet.Testcontainers.Containers;
global using Exchange.Portal.ApplicationCore.Features.Token.Queries;
global using Exchange.Portal.ApplicationCore.Interface;
global using Exchange.Portal.ApplicationCore.Models;
global using Exchange.Portal.Infrastructure.Documents;
global using Exchange.Portal.IntegrationTests.Abstractions;
global using Exchange.Portal.IntegrationTests.Fakes;
global using FluentAssertions;
global using Marten;
global using MediatR;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Mvc.Testing;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.DependencyInjection.Extensions;
global using Microsoft.Extensions.Hosting;
global using Xunit;
global using Xunit.Categories;