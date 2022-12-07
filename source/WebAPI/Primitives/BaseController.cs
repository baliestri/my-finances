// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Microsoft.AspNetCore.Mvc;

namespace MyFinances.WebAPI.Primitives;

/// <summary>
///   A base class for all controllers.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseController : ControllerBase { }
