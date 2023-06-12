import React, { useEffect } from "react";
import { FaTrashAlt } from "react-icons/fa";
import ActionOrder from 'pages/Products/actions/cart';
import { useDispatch, useSelector } from "react-redux";


function PaymentSect() {
  const dispatch = useDispatch();
  const cart = useSelector(({topics}) => topics.CartReducer);
  
  useEffect(() => {
    dispatch(ActionOrder.receiveCart());
    // console.log(cart);
    // console.log([...new Map(cart.list.map(v => [v.product.name, v])).values()]);
  }, [cart.list])
  return (
    <>
      <div className="payment">
        <div className="name">
          <h4>Item</h4>
          <p>Qty</p>
          <p>Price</p>
        </div>

        <div className="price">

          {
            cart.isLoading && (
              <span>Loading...</span>
            )
          }
          {
            !cart.isLoading &&
            [...new Map(cart.list.map(v => [v.product.name, v])).values()].map(product => {
              // console.log(product.product);
              return (
                <article>
                  <div className="pay">
                    <div>
                      <b>{product.product.name}</b>
                      <p>{product.product.price}</p>
                    </div>
                    <p className="qty-box">{cart.list.filter(el => el.product.name == product.product.name).length}</p>
                    <p>${cart.list.filter(el => el.product.name == product.product.name).length * parseFloat(product.product.price.substring(1))}</p>
                  </div>

                  <div className="pay">
                    <input className="order-input" placeholder="Input Order Note" ></input>
                    <span className="trash-box" title="delete"><FaTrashAlt onClick={() => dispatch(ActionOrder.removeProductCart({product}))} /></span> {/* TODO add onclick on delete */}
                  </div>
                </article>
              )
            })
          }


          <figure>
            <div className="last">
              <p className="space">Discount</p>
              <p>Total</p>
            </div>
            <div className="last">
              <p className="space">$0</p>
              <p>${
                cart.list.reduce(
                  (acc, curr) => acc + parseFloat(curr.product.price.substring(1)),
                  0
                )
              }
              </p>
            </div>
          </figure>



          {/* <figure>
            <div className="last">
              <p className="space">Discount</p>
              <p>Sub-total</p>
            </div>
            <div className="last">
              <p className="space">$0</p>
              <p>{total()}</p>
            </div>
          </figure> */}




        </div>
      </div>
    </>
  );
}

export default PaymentSect;