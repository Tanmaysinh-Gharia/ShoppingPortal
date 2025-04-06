# ShoppingPortal

ShoppingPortal is a robust E-Commerce Product & Order Management System built using ASP.NET Core and EF Core. It features an Admin Portal for managing products and orders, and a Customer Portal for browsing, purchasing, and tracking products.

## Trail Users:
ADMIN: admin@shoppingportal.com : Password: Admin@123
Customer: tanmay.sinh@gmail.com : Password: Tanmaysinh@123
Customer: john.doe@example.com : Password: JohnDoe@123

## 📦 Features

### Admin Portal
- Add/Edit/Delete Categories
- Add/Edit/Delete Products
- View/Manage Order Requests (Pending → Shipped → Delivered)
- Bulk product and category management

### Customer Portal
- Browse/Search/Filter products by category
- Add to Cart & Order placement
- Order history tracking
- Cancel pending orders (optional)

## 🧠 Architectural Highlights

- **Service Layer**: Business logic handled in services (not controllers).
- **Repository Pattern**: Encapsulated data access logic.
- **DbContext**: Used at service/repo level for clear separation.
- **LINQ & EF Core Mastery**: All CRUD + advanced queries used.
- **Concurrency Control**: Row versioning to manage concurrent updates.
- **Transactions**: Ensures atomicity and consistency.
- **Loading Types**:
  - **Eager Loading**: Loads related data using `.Include()`.
  - **Lazy Loading**: Loads data on navigation access (proxy-based).
  - **Explicit Loading**: Manually loads related data using `.Entry().Collection().Load()` or similar.

## 🧮 Database Schema (3NF Compliant)

### Users
- UserId (PK)
- Username
- Email (unique)
- PasswordHash
- PhoneNumber
- CreatedAt, UpdatedAt
- IsActive
- UserType (Customer/Admin/Vendor)
- StreetAddress
- PostalCode (FK)

### Addresses
- City, State
- PostalCode (PK)

### Categories
- CategoryId (PK)
- Name, Description
- CreatedBy (FK), CreatedAt

### Products
- ProductId (PK)
- Name, Description, SKU (unique)
- Price, StockQuantity
- CategoryId (FK)
- CreatedBy (FK)
- ImageUrl, CreatedAt, UpdatedAt

### Orders
- OrderId (PK)
- UserId (FK)
- Status (Pending/Shipped/Delivered/Cancelled)
- TotalAmount
- ShippingAddressId (FK)
- CreatedAt, UpdatedAt

### OrderItems
- OrderItemId (PK)
- OrderId (FK), ProductId (FK)
- Quantity

### OrderStatusLog
- LogId (PK)
- OrderId (FK), ProductId (FK, nullable)
- OldStatus, NewStatus
- ChangedBy (FK), ChangedAt

### UserOrders (Optional)
- UserId (FK), OrderId (FK)
- CreatedAt

### ShoppingCarts
- CartId (PK)
- UserId (FK)

### CartItems
- CartItemId (PK)
- CartId (FK), ProductId (FK)
- Quantity
- AddedAt

## 🛠️ Tech Stack

- **Backend**: ASP.NET Core, EF Core
- **Frontend**: Razor Pages / MVC Views
- **ORM**: Entity Framework Core
- **Database**: SQL Server
- **Authentication**: Role-based (Admin, Customer, Vendor)

## ✅ Standards Followed

- **Naming Conventions**:
  - Tables: Plural (e.g., `Users`)
  - Models: Singular (e.g., `User`)
  - Variables: camelCase (e.g., `blackColor`)
  - Properties: PascalCase (e.g., `BlackColor`)
- **Best Practices**:
  - No logic in controllers
  - Constants in a dedicated class : At core layer Constants Folder
  - Clean, formatted LINQ queries : At Repo Layer Not at Service Layer
  - Switch-case instead of chained if-else : Used In Role wise Redirection and Helper/Extention Methods
  - Null checks, exception safety
  - Generic reusable methods
  - Unused code removed
  - Description and comments in each method/model
  - One-line space before/after if, foreach, component calls

## 🔍 Code Highlights & Logical Implementations

- **EF Core Loading**:
  - **Lazy Loading**: Navigation properties configured for dynamic load on access.
  - **Eager Loading**: `.Include()` used for loading related data upfront.
  - **Explicit Loading**: Manual loading via `Entry().Collection().Load()` in performance-critical areas.
- **Many-to-Many Mapping**: Between Products and Categories.
- **Row Versioning**: Used for handling concurrency conflicts.
- **Transactional Integrity**: Critical operations wrapped in EF Core transactions.
- **3NF Database Design**: Ensures scalability and normalized structure.
- **Cart Management**: One-to-many structure for flexible item management.

## 🚀 How to Run

```bash
git clone https://github.com/Tanmaysinh-Gharia/ShoppingPortal.git
cd ShoppingPortal
dotnet restore
# Update connection string in appsettings.json
dotnet ef database update
dotnet run
```

## 🔧 Admin vs Customer Access
Portal	URL	Roles
Admin Portal	http://localhost:5000/admin	Admin
Customer Portal	http://localhost:5000	Customer, Vendor

## 📈 Additional Functionality
Row versioning for concurrency control

EF Core loading (Lazy, Eager, Explicit)

Order cancellation (if pending)

Order status logs

Shopping cart flow with validations

Advanced filtering/searching of products

## 🤝 Contribution
# Steps to contribute
1. Fork the repo
2. Create a feature branch
3. Push your changes
4. Submit a Pull Request


## File Trees
├── ShoppingPortal/
│   ├── .gitattributes
│   ├── .gitignore
│   ├── ShoppingPortal.sln
│   ├── ShoppingPortal.slnLaunch.user
│   ├── .git/
│   │   ├── .COMMIT_EDITMSG.swp
│   │   ├── COMMIT_EDITMSG
│   │   ├── config
│   │   ├── description
│   │   ├── FETCH_HEAD
│   │   ├── HEAD
│   │   ├── index
│   │   ├── ms-persist.xml
│   │   ├── ORIG_HEAD
│   │   ├── packed-refs
│   │   ├── hooks/
│   │   │   ├── applypatch-msg.sample
│   │   │   ├── commit-msg.sample
│   │   │   ├── fsmonitor-watchman.sample
│   │   │   ├── post-update.sample
│   │   │   ├── pre-applypatch.sample
│   │   │   ├── pre-commit.sample
│   │   │   ├── pre-merge-commit.sample
│   │   │   ├── pre-push.sample
│   │   │   ├── pre-rebase.sample
│   │   │   ├── pre-receive.sample
│   │   │   ├── prepare-commit-msg.sample
│   │   │   ├── push-to-checkout.sample
│   │   │   ├── sendemail-validate.sample
│   │   │   ├── update.sample
│   │   ├── info/
│   │   │   ├── exclude
│   │   │   ├── refs
│   │   ├── logs/
│   │   │   ├── HEAD
│   │   │   ├── refs/
│   │   ├── objects/
│   │   │   ├── c4/
│   │   │   │   ├── c2aa64f34537037c326db531ba191860cb8b94
│   │   │   ├── info/
│   │   │   │   ├── commit-graph
│   │   │   │   ├── packs
│   │   │   ├── pack/
│   │   │   │   ├── pack-ae0341aaeb1e1d0881a2ed11c6daaff5d0a36c42.idx
│   │   │   │   ├── pack-ae0341aaeb1e1d0881a2ed11c6daaff5d0a36c42.pack
│   │   │   │   ├── pack-ae0341aaeb1e1d0881a2ed11c6daaff5d0a36c42.rev
│   │   ├── refs/
│   │   │   ├── heads/
│   │   │   │   ├── v2
│   │   │   ├── remotes/
│   │   │   ├── tags/
│   ├── .github/
│   │   ├── workflows/
│   ├── .vs/
│   │   ├── slnx.sqlite
│   │   ├── VSWorkspaceState.json
│   │   ├── ProjectEvaluation/
│   │   │   ├── shoppingportal.metadata.v9.bin
│   │   │   ├── shoppingportal.projects.v9.bin
│   │   │   ├── shoppingportal.strings.v9.bin
│   │   ├── ShoppingPortal/
│   │   │   ├── config/
│   │   │   │   ├── applicationhost.config
│   │   │   ├── copilot-chat/
│   │   │   ├── CopilotIndices/
│   │   │   ├── DesignTimeBuild/
│   │   │   │   ├── .dtbcache.v2
│   │   │   ├── FileContentIndex/
│   │   │   │   ├── 67b5877d-0355-4312-adfe-9091b202cd77.vsidx
│   │   │   │   ├── 6ed01c48-2c50-424f-b3c6-f545cc822a7b.vsidx
│   │   │   │   ├── f3f640ad-90c6-446c-9961-a63eb465a36c.vsidx
│   │   │   │   ├── fc3602c7-6b54-4e03-bc59-50f27b61240a.vsidx
│   │   │   │   ├── fdb9af0e-deea-4c7e-ad44-d3e02500acfe.vsidx
│   │   │   ├── v17/
│   │   │   │   ├── .futdcache.v2
│   │   │   │   ├── .suo
│   │   │   │   ├── .wsuo
│   │   │   │   ├── DocumentLayout.backup.json
│   │   │   │   ├── DocumentLayout.json
│   ├── ShoppingPortal/
│   │   ├── appsettings.Development.json
│   │   ├── appsettings.json
│   │   ├── Program.cs
│   │   ├── ShoppingPortal.Web.csproj
│   │   ├── ShoppingPortal.Web.csproj.user
│   │   ├── bin/
│   │   │   ├── Debug/
│   │   │   ├── Release/
│   │   ├── Controllers/
│   │   │   ├── AccountController.cs
│   │   │   ├── AdminController.cs
│   │   │   ├── CartController.cs
│   │   │   ├── CustomerController.cs
│   │   │   ├── HomeController.cs
│   │   │   ├── OrderController.cs
│   │   │   ├── ProductController.cs
│   │   ├── Models/
│   │   │   ├── CategoryViewModel.cs
│   │   │   ├── ErrorViewModel.cs
│   │   │   ├── LoginViewModel.cs
│   │   │   ├── PagingInfo.cs
│   │   │   ├── ProductListViewModel.cs
│   │   │   ├── ProductViewModel.cs
│   │   │   ├── SignupViewModel.cs
│   │   │   ├── User.cs
│   │   │   ├── CustomValidation/
│   │   │   │   ├── UniqueEmailAttribute.cs
│   │   │   │   ├── ValidCountryandState.cs
│   │   ├── obj/
│   │   │   ├── project.assets.json
│   │   │   ├── project.nuget.cache
│   │   │   ├── ShoppingPortal.csproj.nuget.dgspec.json
│   │   │   ├── ShoppingPortal.csproj.nuget.g.props
│   │   │   ├── ShoppingPortal.csproj.nuget.g.targets
│   │   │   ├── ShoppingPortal.Web.csproj.nuget.dgspec.json
│   │   │   ├── ShoppingPortal.Web.csproj.nuget.g.props
│   │   │   ├── ShoppingPortal.Web.csproj.nuget.g.targets
│   │   │   ├── Debug/
│   │   │   ├── Release/
│   │   ├── Properties/
│   │   │   ├── launchSettings.json
│   │   ├── Views/
│   │   │   ├── _ViewImports.cshtml
│   │   │   ├── _ViewStart.cshtml
│   │   │   ├── Account/
│   │   │   │   ├── Login.cshtml
│   │   │   │   ├── Signup.cshtml
│   │   │   ├── Admin/
│   │   │   │   ├── AddCategory.cshtml
│   │   │   │   ├── AddProduct.cshtml
│   │   │   │   ├── Categories.cshtml
│   │   │   │   ├── EditCategory.cshtml
│   │   │   │   ├── EditProduct.cshtml
│   │   │   │   ├── Index.cshtml
│   │   │   │   ├── Orders.cshtml
│   │   │   │   ├── Products.cshtml
│   │   │   │   ├── _OrderDetailsPartial.cshtml
│   │   │   ├── Cart/
│   │   │   │   ├── Index.cshtml
│   │   │   │   ├── Shared_CartPartial.cshtml
│   │   │   ├── Home/
│   │   │   │   ├── Index.cshtml
│   │   │   │   ├── Privacy.cshtml
│   │   │   ├── Order/
│   │   │   │   ├── Index.cshtml
│   │   │   │   ├── _OrderCard.cshtml
│   │   │   ├── Product/
│   │   │   │   ├── Index.cshtml
│   │   │   │   ├── _ProductCards.cshtml
│   │   │   ├── Shared/
│   │   │   │   ├── Error.cshtml
│   │   │   │   ├── _AdminLayout.cshtml
│   │   │   │   ├── _Layout.cshtml
│   │   │   │   ├── _Layout.cshtml.css
│   │   │   │   ├── _ValidationScriptsPartial.cshtml
│   │   ├── wwwroot/
│   │   │   ├── favicon.ico
│   │   │   ├── css/
│   │   │   │   ├── site.css
│   │   │   ├── js/
│   │   │   │   ├── admin.js
│   │   │   │   ├── cart.js
│   │   │   │   ├── orders.js
│   │   │   │   ├── product.js
│   │   │   │   ├── site.js
│   │   │   ├── lib/
│   │   │   ├── media/
│   ├── ShoppingPortal.Core/
│   │   ├── ShoppingPortal.Core.csproj
│   │   ├── bin/
│   │   │   ├── Debug/
│   │   │   ├── Release/
│   │   ├── Constants/
│   │   │   ├── AppConstants.cs
│   │   │   ├── ProductConstants.cs
│   │   ├── DTOs/
│   │   │   ├── AddToCartDto.cs
│   │   │   ├── CartDto.cs
│   │   │   ├── CategoryDto.cs
│   │   │   ├── LoginDto.cs
│   │   │   ├── LoginRole.cs
│   │   │   ├── NotFoundException.cs
│   │   │   ├── OrderDto.cs
│   │   │   ├── ProductDto.cs
│   │   │   ├── UpdateQuantityDto.cs
│   │   │   ├── UserDto.cs
│   │   │   ├── ValidationException.cs
│   │   │   ├── Admin/
│   │   │   │   ├── AdminDashboardDto.cs
│   │   │   │   ├── CategoryViewModel.cs
│   │   │   │   ├── ProductViewModel.cs
│   │   ├── Enums/
│   │   │   ├── OrderStatusEnum.cs
│   │   │   ├── UserTypeEnum.cs
│   │   ├── Exceptions/
│   │   │   ├── InsufficientStockException.cs
│   │   ├── Extentions/
│   │   │   ├── EnumExtensions.cs
│   │   ├── Helpers/
│   │   │   ├── PaginatedResult.cs
│   │   │   ├── PasswordHasher.cs
│   │   ├── Interfaces/
│   │   │   ├── IAdminService.cs
│   │   │   ├── ICartService.cs
│   │   │   ├── IOrderService.cs
│   │   │   ├── IProductService.cs
│   │   │   ├── IUserService.cs
│   │   ├── Models/
│   │   │   ├── JwtConfig.cs
│   │   │   ├── OrderListViewModel.cs
│   │   │   ├── PagingInfo.cs
│   │   ├── obj/
│   │   │   ├── project.assets.json
│   │   │   ├── project.nuget.cache
│   │   │   ├── ShoppingPortal.Core.csproj.nuget.dgspec.json
│   │   │   ├── ShoppingPortal.Core.csproj.nuget.g.props
│   │   │   ├── ShoppingPortal.Core.csproj.nuget.g.targets
│   │   │   ├── Debug/
│   │   │   ├── Release/
│   │   ├── Services/
│   │   │   ├── BaseService.cs
│   ├── ShoppingPortal.Data/
│   │   ├── ShoppingPortal.Data.csproj
│   │   ├── bin/
│   │   │   ├── Debug/
│   │   │   ├── Release/
│   │   ├── Configurations/
│   │   │   ├── AddressConfiguration.cs
│   │   │   ├── CartItemConfiguration.cs
│   │   │   ├── CategoryConfiguration.cs
│   │   │   ├── OrderConfiguration.cs
│   │   │   ├── OrderItemConfiguration.cs
│   │   │   ├── OrderStatusLogConfiguration.cs
│   │   │   ├── ProductCategoryConfiguration.cs
│   │   │   ├── ProductConfiguration.cs
│   │   │   ├── ShoppingCartConfiguration.cs
│   │   │   ├── UserConfiguration.cs
│   │   ├── Context/
│   │   │   ├── ApplicationDbContext.cs
│   │   ├── Entities/
│   │   │   ├── Address.cs
│   │   │   ├── CartItem.cs
│   │   │   ├── Category.cs
│   │   │   ├── Order.cs
│   │   │   ├── OrderItem.cs
│   │   │   ├── OrderStatusLog.cs
│   │   │   ├── Product.cs
│   │   │   ├── ProductCategory.cs
│   │   │   ├── ShoppingCart.cs
│   │   │   ├── User.cs
│   │   ├── Interfaces/
│   │   │   ├── IAdminRepository.cs
│   │   │   ├── ICartRepository.cs
│   │   │   ├── ICategoryRepository.cs
│   │   │   ├── IOrderRepository.cs
│   │   │   ├── IProductRepository.cs
│   │   │   ├── IUserRepository.cs
│   │   ├── Migrations/
│   │   │   ├── 20250403024445_addingDummyEntriesInAllTables.cs
│   │   │   ├── 20250403024445_addingDummyEntriesInAllTables.Designer.cs
│   │   │   ├── 20250403024820_updatedusingSentinal.cs
│   │   │   ├── 20250403024820_updatedusingSentinal.Designer.cs
│   │   │   ├── 20250403040653_removedimageurl.cs
│   │   │   ├── 20250403040653_removedimageurl.Designer.cs
│   │   │   ├── 20250404112248_addedRowversion.cs
│   │   │   ├── 20250404112248_addedRowversion.Designer.cs
│   │   │   ├── 20250405084730_AddedStatusToOrderItem.cs
│   │   │   ├── 20250405084730_AddedStatusToOrderItem.Designer.cs
│   │   │   ├── 20250405123628_AddedmtonRelationshiptoProductCategories.cs
│   │   │   ├── 20250405123628_AddedmtonRelationshiptoProductCategories.Designer.cs
│   │   │   ├── 20250406095026_SeededDataAddedSomeInactiveUsers.cs
│   │   │   ├── 20250406095026_SeededDataAddedSomeInactiveUsers.Designer.cs
│   │   │   ├── 20250406102724_addedProductCategory.cs
│   │   │   ├── 20250406102724_addedProductCategory.Designer.cs
│   │   │   ├── 20250406185542_addusers.cs
│   │   │   ├── 20250406185542_addusers.Designer.cs
│   │   │   ├── ApplicationDbContextModelSnapshot.cs
│   │   ├── obj/
│   │   │   ├── project.assets.json
│   │   │   ├── project.nuget.cache
│   │   │   ├── ShoppingPortal.Data.csproj.nuget.dgspec.json
│   │   │   ├── ShoppingPortal.Data.csproj.nuget.g.props
│   │   │   ├── ShoppingPortal.Data.csproj.nuget.g.targets
│   │   │   ├── Debug/
│   │   │   ├── Release/
│   │   ├── Repositories/
│   │   │   ├── AdminRepository.cs
│   │   │   ├── CartRepository.cs
│   │   │   ├── CategoryRepository.cs
│   │   │   ├── OrderRepository.cs
│   │   │   ├── ProductRepository.cs
│   │   │   ├── UserRepository.cs
│   │   ├── Seeds/
│   │   │   ├── AddressSeedData.cs
│   │   │   ├── CartItemSeedData.cs
│   │   │   ├── CategorySeedData.cs
│   │   │   ├── OrderItemSeedData.cs
│   │   │   ├── OrderSeedData.cs
│   │   │   ├── OrderStatusLogSeedData.cs
│   │   │   ├── ProductCategorySeedData.cs
│   │   │   ├── ProductSeedData.cs
│   │   │   ├── ShoppingCartSeedData.cs
│   │   │   ├── UserSeedData.cs
│   ├── ShoppingPortal.Services/
│   │   ├── DependencyInjection.cs
│   │   ├── ShoppingPortal.Services.csproj
│   │   ├── AdminServices/
│   │   │   ├── AdminService.cs
│   │   ├── bin/
│   │   │   ├── Debug/
│   │   │   ├── Release/
│   │   ├── CartServices/
│   │   │   ├── CartService.cs
│   │   ├── obj/
│   │   │   ├── project.assets.json
│   │   │   ├── project.nuget.cache
│   │   │   ├── ShoppingPortal.Services.csproj.nuget.dgspec.json
│   │   │   ├── ShoppingPortal.Services.csproj.nuget.g.props
│   │   │   ├── ShoppingPortal.Services.csproj.nuget.g.targets
│   │   │   ├── Debug/
│   │   │   ├── Release/
│   │   ├── OrderServices/
│   │   │   ├── OrderService.cs
│   │   ├── ProductServices/
│   │   │   ├── ProductService.cs
│   │   ├── Profiles/
│   │   │   ├── AdminProfile.cs
│   │   │   ├── AdminViewModelProfile .cs
│   │   │   ├── CartProfile.cs
│   │   │   ├── CategoryProfile.cs
│   │   │   ├── ProductProfile.cs
│   │   │   ├── UserProfile.cs
│   │   ├── UserServices/
│   │   │   ├── CountryService.cs
│   │   │   ├── UserService.cs