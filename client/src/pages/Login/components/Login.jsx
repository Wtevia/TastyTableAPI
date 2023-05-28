import React from "react";
import { Container, H } from '../styles/LoginStyles';
import AuthorizationForm from "components/AuthorizationForm/AuthorizationForm";

const Login = ({
    children,
}) => {
    return(
        <Container>
            <AuthorizationForm />
        </Container>
    );
};

export default Login;