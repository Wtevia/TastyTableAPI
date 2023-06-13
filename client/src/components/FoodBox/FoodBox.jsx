import React from "react";
import { useDispatch } from "react-redux";

import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import ActionOrder from 'pages/Products/actions/cart';




function FoodBox({ imgSrc, title, price, description }) {
  const dispatch = useDispatch();
  
  const notify = (product) => {
    dispatch(ActionOrder.addProductCart({product}));
    // console.log(product);
    toast.success('Item has been added to cart', {
      position: "top-center",
      autoClose: 3000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
      theme: "light",
      });
  }
  // const handleClick = async () => {
  //   alert("Item has been added to cart");
  // };

  return (
    <div className="details">
      <img src={imgSrc} alt="" className="details-img" />
      <div className="food-name">
        <h2>{title}</h2>
      </div>

      <div className="food-details">
        <div>
          <p>
            Description: {description}
          </p>
          <p>
            Price: {price}
          </p>
          <p>Available</p>
        </div>
      </div>

      <div className="cart-btn">
        {/* <button onClick={handleClick} className="btn">
          Add to cart
        </button> */}
        <button onClick={() => notify({name: title, price})} className="btn">Add to cart</button>
      </div>
      <ToastContainer />
    </div>
  );
}

export default FoodBox;