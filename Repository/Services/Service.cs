using CNaturalApi.Context;
using CNaturalApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using CNaturalApi.Repository.IServices;
namespace CNaturalApi.Repository.Services
{
    // PREGUNTAR LO DEL METODO UPDATE.
    // (YA)ME FALTA TERMINAR CON (LO QUE FALTA)INVESTMENT(YA)
    // (YA)ME FALTA HACER EL METODO DE BORRAR VENTA EN PRODUCTO(YA)
    // ME FALTA LO DE QUARTZ Y HACER LOS CONTROLADORES(CASI TODOS).
    // VER QUE HAGO CON LO DE BORRRAR ACCOUNTANCY
    // PREGUNTAR LO DE QUE PASA SI EL BUYER ESTA BORRADO Y AGREGAN UNO IGUAL.
    public class Service : IProductService, ISaleService, IAccountancyService, IInvestmentService, IBuyerService
    {
        CNaturalContext _context;
        public Service(CNaturalContext context)
        {
            _context = context;


        }
        #region Product region
        public async Task<Product> GetProduct(int productId)
        {
            try
            {
                Product product = await
                    _context.Products.Include(s=>s.Sales).FirstOrDefaultAsync(p => p.Id == productId);
                return product;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                IEnumerable<Product> products =
                    await _context.Products.ToListAsync();
                return products;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Product> AddProduct(Product product)
        {

            bool productExists = await ProductAlreadyExists(product.Name);
            if (!productExists)
            {
                product.Investments = new List<Investment>();
                product.Sales = new List<Sale>();  // lo hago con una lista?
                var addedProduct = await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return addedProduct.Entity;

            }
            else return null;
            //Arreglar aqui.


            //voy a poner el id de las contablidad como la fecha que es, no se repetira nunca y significa algo
            //cuando quite la contabilidad vieja tengo que quitarla tambien de la lista de inversion y de ventas

        }
        public async Task<Product> UpdateProduct(int id, Product product)
        {
            try
            {

                if (id != product.Id)
                    return null;

                else
                {
                    var updatedProduct = _context.Products.Update(product);
                    //_context.Entry(product).State = EntityState.Modified; asi funciona
                    await _context.SaveChangesAsync();
                    return updatedProduct.Entity;
                    //    var p = await _context.Products.FindAsync(id);
                    //    var updatedProduct = _context.Products.Update(p);
                    //    await _context.SaveChangesAsync();
                    //    return updatedProduct.Entity;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                var product = await _context.Products.FindAsync(productId);
                if (product != null && !product.IsDeleted)
                {
                    product.IsDeleted = true;
                    product.Count = 0;
                    await _context.SaveChangesAsync();
                    return true;
                }
                else return false;
            }
            catch (Exception)
            {
                return true;
            }
        }
        public async Task<bool> ProductAlreadyExists(string name)
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                if (products != null)
                {
                    var p = products.Find(p => p.Name == name);
                    if (p != null)
                        return true;
                    else return false;
                }
                return false;
            }
            catch (Exception)
            {
                //tengo que diferenciar la excepcion de un valor de retorno.
                return false;
            }

        }



        //public async Task<bool> IsSameProductName(string name) 
        //{
        //    //puede que lo que tenga que devolver sea el elemento.
        //    try
        //    {
        //        var product = await _context.Products.FindAsync(name);
        //        if (product == null)
        //            return false;
        //        return true;
        //    }
        //    catch  
        //    {
        //        return false;
        //    }
        //}

        //en un metodo agrego la contabilidad y en otro genero una nueva.

        //cuando agregue o modifique inversiones tengo que acutalizar lo demas
        //preguntar lo de la clase comprador.
        #endregion
        #region Sale region
        public async Task<Sale> GetSale(int saleId)
        {
            try
            {
                var sale = await _context.Sales
                    .Include(sP => sP.Product).ThenInclude(pS => pS.Sales)
                    .Include(sB => sB.Buyer).ThenInclude(bS => bS.Sales).ThenInclude(bP=>bP.Product)
                    .Include(sA => sA.Accountancy)
                    .FirstOrDefaultAsync(s => s.Id == saleId);
                return sale;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<IEnumerable<Sale>> GetAllSales()
        {
            try
            {
                var sales = await _context.Sales.ToListAsync();
                return sales;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Sale> AddSale(Sale sale)
        {

            
            try
            {
                int count = sale.Count;
                sale.Accountancy = await _context.Accountancies
                    .Include(aS => aS.Sales)
                    .OrderBy(a => a.Id).LastAsync();
                sale.Product = await _context.Products
                    .Include(ps => ps.Sales)
                    .FirstOrDefaultAsync(p => p.Id == sale.Product.Id);
                sale.Buyer = await _context.Buyers
                    .Include(bs => bs.Sales).FirstOrDefaultAsync(b => b.Id == sale.Buyer.Id);
                /*FindAsync(sale.Buyer.Id);*/
                if (sale.Accountancy == null || sale.Product == null || sale.Product.IsDeleted || sale.Buyer == null || sale.Buyer.IsDeleted)
                    throw new Exception();
                else
                {
                    if (sale.Product.Count < sale.Count)
                        throw new Exception();
                    else
                    {
                        sale.Product.Count = sale.Product.Count - sale.Count;
                        if (sale.Product.Sales == null)
                            sale.Product.Sales = new List<Sale>();
                        sale.Product.Sales.Add(sale);
                        if (sale.Accountancy.Sales == null)
                            sale.Accountancy.Sales = new List<Sale>();
                        sale.Accountancy.EarnedMoney = sale.Accountancy.EarnedMoney + sale.Price;
                        sale.Accountancy.Sales.Add(sale);

                        if (sale.Buyer.Sales == null)
                            sale.Buyer.Sales = new List<Sale>();
                        var x = 0;
                        sale.Buyer.Sales.Add(sale);
                        var addedSale = await _context.Sales.AddAsync(sale);
                        await _context.SaveChangesAsync();
                        return addedSale.Entity;

                    }
                }
            }

            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<Sale> UpdateSale(int saleId, Sale sale)
        {
            var currentTime = DateTime.Now;
            if (saleId != sale.Id)
                return null;
            else
            {
                try
                {
                    var s = await _context.Sales
                        .Include(sP => sP.Product).ThenInclude(p=>p.Sales)
                        .FirstOrDefaultAsync(s => s.Id == saleId);

                    var difference = currentTime.Subtract(s.Date);
                    //if (difference.Days < 1 && difference.Hours <= 1) esto es para descomentar despues.
                    //{
                    //var p = sale.Product;
                    //cambio var p = sale.Product para trabjar directamente con s.Product (esto es de cambios que hice antes, obvialo)
                    s.Product.Count = s.Product.Count + s.Count;
                    if (!CanUpdate(s.Product.Count, sale.Count)) //esto es si hay mas en stock que lo que se va a vender, es un if tonto realmente
                        throw new Exception("Aqui agrego algo");
                    s.Product.Count = s.Product.Count - sale.Count;
                    //Si aqui actualizo manual en la lista de vemtas del producto, servira, pero no es la idea.
                    //actualiza esto(Count) del producto porque esta literal(que lo actualice)
                    var updatedSale = _context.Sales.Update(sale);   //voy a pasar como parametro la venta que me dan
                    //aqui dice que busca todas las entidades a las que haga referencia(usando sales.update, no se la diferencia
                    //que tiene con context.Update nada mas)
                    //si paso como parametro la venta que me dan van a guardarse los parametros feos esos que paso en el swagger
                    await _context.SaveChangesAsync();
                    return updatedSale.Entity;
                    //}
                    //else throw new Exception("Aqui digo que ya ha pasado mas de una hora de realizada la venta");
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Esta incompleto
        /// Just you can delete a sale if has past least a hour since it do
        /// </summary>
        /// <param name="saleId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteSale(int saleId)
        {
            // solo se puede borrar una venta si ha pasado menos de 2 hora desde que se hizo
            DateTime currentTime = DateTime.Now;
            try
            {
                var sale = await _context.Sales.FindAsync(saleId);
                if (sale != null)
                {
                    var difference = currentTime.Subtract(sale.Date);
                    //si ha pasado menos de 2 horas de la realizacion de la venta, ent se puede cancelar
                    if (difference.Days < 1 && difference.Hours <= 1)
                    {
                        var product = sale.Product;

                        //borro la venta de la lista de ventas que tiene el producto
                        DeleteSaleInProduct(product, saleId);
                        //actualizco el producto
                        await UpdateProduct(product.Id, product);
                        //devuelvo el dinero que ganó
                        double earnedMoney = sale.Accountancy.EarnedMoney;
                        Accountancy acc = sale.Accountancy;
                        acc.EarnedMoney = earnedMoney - sale.Price;
                        DeleteSaleInAccountancy(acc, saleId);
                        //actualizo la contabilidad
                        await UpdateAccountancy(acc.Id, acc);
                        //actualizo el buyer
                        DeleteSaleInBuyer(sale.Buyer, saleId);
                        await UpdateBuyer(sale.Buyer.Id, sale.Buyer);
                        //borro la venta de la lista de ventas
                        _context.Sales.Remove(sale);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public bool CanUpdate(int stockCount, int count)
        {
            if (stockCount < count)
                return false;
            return true;
        }
        #endregion
        #region Accountancy region(Incomplete)
        public async Task<Accountancy> GetAccountancy(int accountancyId)
        {
            try
            {
                Accountancy accountancy = await _context.Accountancies.FindAsync(accountancyId);
                if (accountancy != null)
                    return accountancy;
                else throw new Exception();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Tengo duda con el first
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<Accountancy> GetAccountancyByDate(DateTime date)
        {
            try
            {
                //no se si deba ser asi
                var accountancy = await _context.Accountancies.FirstAsync(a =>
               a.Date.Month == date.Month && a.Date.Year == date.Year);
                if (accountancy != null)
                    return accountancy;
                else throw new Exception();

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Accountancy>> GetAllAccountancies()
        {
            try
            {
                var accountancies = await _context.Accountancies
                    .Include(aS => aS.Sales).ThenInclude(sP => sP.Product)
                    .Include(aI => aI.Investments)
                    .ToListAsync();
                if (accountancies != null)
                    return accountancies;
                else throw new Exception();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<Accountancy> AddAccountancy(Accountancy accountancy)
        {
            if (DateTime.Now.Day == 1)
            {
                try
                {
                    //pongo la fecha actual
                    accountancy.Date = DateTime.Now;
                    //creo la lista de inversiones
                    accountancy.Investments = new List<Investment>();
                    //creo la lista de ventas
                    accountancy.Sales = new List<Sale>();
                    var addedAccountancy = await _context.Accountancies.AddAsync(accountancy);
                    await _context.SaveChangesAsync();
                    return addedAccountancy.Entity;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else return null;

        }

        public async Task<Accountancy> UpdateAccountancy(int accountancyId, Accountancy accountancy)
        {
            if (accountancyId != accountancy.Id)
                return null;
            try
            {
                var updatedAccountancy = _context.Accountancies.Update(accountancy);
                await _context.SaveChangesAsync();
                return updatedAccountancy.Entity;

            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion
        #region Investment region(Incomplete, Quartz)
        public async Task<Investment> GetInvestment(int investmentId)
        {
            try
            {
                var investment = await _context.Investments.FindAsync(investmentId);
                if (investment != null)
                    return investment;

                else throw new Exception();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Investment>> GetAllInvestments()
        {
            try
            {
                var investments = await _context.Investments.ToListAsync();
                if (investments != null)
                    return investments;
                else throw new Exception();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Investment> AddInvestment(Investment investment)
        {
            
            //El producto no se modifica hasta que la inversion no haya llegado.
            investment.TaskDate = DateTime.Now;
            //se agrega a accountancy cuando llegue el producto, ademas, se aumenta el producto en stock cuando llegue.
            //faltan cosas en otro metodo.
            if (!(investment.ArrivalDate > investment.TaskDate))
                return null;
            var addedInvestment = await _context.Investments.AddAsync(investment);
            await _context.SaveChangesAsync();
            return addedInvestment.Entity;

        }

        public async Task<Investment> UpdateInvestment(int investmentId, Investment investment)
        {
            var currentTime = DateTime.Now;

            if (investment.Id != investmentId)
                return null;
            try
            {
                var invest = await _context.Investments.FindAsync(investmentId);
                if (invest != null)
                {
                    var difference = currentTime.Subtract(invest.TaskDate);
                    if (difference.Days < 1 && difference.Hours <= 1 && !invest.IsArrived)
                    {
                        //it can do
                        var updatedInvestment = _context.Investments.Update(investment);
                        await _context.SaveChangesAsync();
                        return updatedInvestment.Entity;
                    }
                    else throw new Exception();
                }
                else throw new Exception();

            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        }
        /// <summary>
        /// The task should be cancel if and only if the day is equal than the day that the task was make
        /// </summary>
        /// <param name="investmentId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteInvestment(int investmentId)
        {
            var currentTime = DateTime.Now;
            try
            {
                var investment = await _context.Investments.FindAsync(investmentId);
                if (investment != null)
                {
                    var difference = currentTime.Subtract(investment.TaskDate);
                    //si se ha realizado la inversion hace menos de una hora entonces se puede eliminar.
                    if (difference.Days < 1 && difference.Hours <= 1)
                    {
                        Product product = investment.Product;
                        if (product != null)
                        {
                            //borro la inversion del producto
                            DeleteInvestmentInProduct(product, investmentId);
                            //actualizo el producto
                            await UpdateProduct(product.Id, product);
                            //borro la inversion de la contabilidad
                            DeleteInvestmentInAccountancy(investment.Accountancy, investmentId);
                            //actualizo la contabilidad
                            await UpdateAccountancy(investment.Accountancy.Id, investment.Accountancy);
                            _context.Investments.Remove(investment);
                            await _context.SaveChangesAsync();
                            return true;
                        }
                        return false;
                    }
                    else return false;

                }
                else return false;

            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
        #region Buyer region
        public async Task<Buyer> AddBuyer(Buyer buyer)
        {
            try
            {
                bool buyerExists = await BuyerAlreadyExists(buyer);
                if (!buyerExists)
                {
                    buyer.Sales = new List<Sale>();
                    var addedBuyer = await _context.Buyers.AddAsync(buyer);
                    await _context.SaveChangesAsync();
                    return addedBuyer.Entity;
                }
                else
                {
                    //puede ser que exista pero que este "borrado".
                    //no se si es correcto hacer esto.
                    //var deletedBuyer = await _context.Buyers.FindAsync(  buyer.Name, buyer.Mobile, buyer.Address );
                    var deletedBuyer = _context.Buyers.ToList().Find(b =>
                    b.Name == buyer.Name && b.Mobile == buyer.Mobile && b.Address == buyer.Address && buyer.IsDeleted);


                    if (deletedBuyer != null)
                    {
                        //PREGUNTAR ESTO AQUI.
                        deletedBuyer.IsDeleted = false;
                        await _context.SaveChangesAsync();
                        return deletedBuyer;
                    }
                    else throw new Exception();
                }
            }
            catch (Exception)
            {
                return null;

            }
        }

        public async Task<Buyer> UpdateBuyer(int buyerId, Buyer buyer)
        {
            //No se puede modificar el nombre del comprador, lo que se puede modificar es el correo y el telefono.
            try
            {
                if (buyerId == buyer.Id)
                {
                    //var b = await _context.Buyers.FindAsync(buyer.Id);
                    ////DUDA AQUI, USO b O buyer.
                    //var updatedBuyer = _context.Buyers.Update(b);
                    //var b = _context.Entry(buyer);
                    var updatedBuyer = _context.Buyers.Update(buyer);
                    await _context.SaveChangesAsync();
                    return updatedBuyer.Entity;
                }
                else throw new Exception();
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<Buyer> GetBuyer(int buyerId)
        {
            try
            {
                var buyer = await _context.Buyers.FindAsync(buyerId);
                if (buyer != null && !buyer.IsDeleted)
                    return buyer;
                else throw new Exception();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Buyer>> GetBuyers()
        {
            try
            {
                var buyers = await _context.Buyers.ToListAsync();
                var noDeleted = buyers.Where(b => !b.IsDeleted);
                if (noDeleted != null)
                    return noDeleted;
                else throw new Exception();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteBuyer(int buyerId)
        {
            try
            {
                var buyer = await _context.Buyers.FindAsync(buyerId);
                if (buyer != null && !buyer.IsDeleted)
                {
                    buyer.IsDeleted = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                else throw new Exception();
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> BuyerAlreadyExists(Buyer buyer)
        {
            //false=>que no existe en buyer.

            try
            {
                var buyers = await _context.Buyers.ToListAsync();
                if (buyers != null)
                {
                    var buyerInList = buyers.Find(b => b.Name == buyer.Name && b.Mobile == buyer.Mobile
                    && b.Address == buyer.Address && b.Id != buyer.Id);
                    //si no existe un buyer que cumpla lo anterior entonces se devuelve false.
                    if (buyerInList == null)
                        return false;
                    else return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }



        #endregion
        #region Auxiliar Methods
        bool DeleteSaleInProduct(Product product, int saleId)
        {
            //Arreglar esto aqui tambien.
            var s = product.Sales.First(x => x.Id == saleId);
            if (s != null)
            {
                bool isRemoved = product.Sales.ToList().Remove(s);
                //UpdateProduct(product.Id, product);
                return isRemoved;
            }
            else return false;
        }
        bool DeleteInvestmentInProduct(Product product, int investmentId)
        {
            var i = product.Investments.First(x => x.Id == investmentId);
            if (i != null)
            {
                bool isRemoved = product.Investments.ToList().Remove(i);
                //await UpdateProduct(product.Id, product);
                return isRemoved;

            }
            else return false;
        }
        bool DeleteInvestmentInAccountancy(Accountancy acc, int investmentId)
        {

            var i = acc.Investments.First(x => x.Id == investmentId);
            if (i != null)
            {
                bool isRemoved = acc.Investments.ToList().Remove(i);
                //await UpdateAccountancy(acc.Id, acc);
                return isRemoved;

            }
            else return false;

        }
        bool DeleteSaleInAccountancy(Accountancy acc, int saleId)
        {
            var s = acc.Sales.First(x => x.Id == saleId);
            if (s != null)
            {
                bool isRemoved = acc.Sales.ToList().Remove(s);
                //await UpdateAccountancy(acc.Id, acc);
                return isRemoved;

            }
            else return false;
        }
        bool DeleteSaleInBuyer(Buyer buyer, int saleId)
        {
            var s = buyer.Sales.First(x => x.Id == saleId);
            if (s != null)
            {
                bool isRemoved = buyer.Sales.ToList().Remove(s);
                return isRemoved;
            }
            return false;
        }



        #endregion

    }

}
