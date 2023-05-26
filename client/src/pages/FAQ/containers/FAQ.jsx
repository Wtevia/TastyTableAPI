import React, { useState } from "react";

import { IconContext } from 'react-icons';
import { FaAngleDown, FaAngleUp } from "react-icons/fa";
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import { Question } from "constants/Question";
import { AccordionSection, Container, Wrap, Dropdown } from '../styles/FAQStyles';

const notify = () => {
    toast("Form is not yet active")
}



const FAQ = ({
    children
}) => {
    const [clicked, setClicked] = useState(false);

    const toggle = index => {
        if (clicked === index) {
            //if clicked question is already active, then close it
            return setClicked(null);
        }

        setClicked(index);
    };

    return (
        <>

            <IconContext.Provider value={{ color: '#F17E6C', size: '20px' }}>

                <AccordionSection>
                    <h2 style={{ color: "#fff", marginBottom: "1.5rem", textAlign: "center" }}>Frequently Asked Questions</h2>
                    <Container>
                        {Question.map((item, index) => {
                            return (
                                <>
                                    <Wrap onClick={() => toggle(index)} key={index}>
                                        <h1>{item.question}</h1>
                                        <span>{clicked === index ? <FaAngleUp /> : <FaAngleDown  />}</span>
                                    </Wrap>
                                    {clicked === index ? (
                                        <Dropdown>
                                            <p>{item.answer}</p>
                                        </Dropdown>
                                    ) : null}
                                </>
                            );
                        })}
                    </Container>
                    <h3 style={{ color: "#fff", marginTop: "2.5rem" }}>Still have questions?</h3> 
                    <p style={{ color: "#fff", marginTop: "1.3rem" }}>Can't find the answer you're looking for? Kindly fill the form below</p>
                    <p style={{ color: "#fff", marginTop: "1rem" }}>Want to file a complaint? Fill the <span style={{color: "#F17E6C", cursor: "pointer", textDecoration: "underline"}} onClick={notify}>form</span> to get answers ASAP</p>
                </AccordionSection>
            </IconContext.Provider>
            <ToastContainer />
        </>
    );
};

export default FAQ;