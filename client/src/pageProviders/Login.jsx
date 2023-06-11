import { useSelector } from 'react-redux';
import React, { useEffect } from 'react';
import useChangePage from 'hooks/useChangePage';
import LoginPage from 'pages/Login';
import PageContainer from 'components/PageContainer';

import * as PAGES from 'constants/pages';

const Login = () => {
  const user = useSelector(({ user }) => user);
  const changePage = useChangePage();
  useEffect(() => {
    
    if (user.isAuthorized) {
      changePage({
        path: `/${PAGES.PRODUCTS}`,
      });
    }
  }, [user.isAuthorized]);

  return (
    user.isAuthorized
    ? null
    : (
      <PageContainer>
        <LoginPage />
      </PageContainer>
    )
  );
};

export default Login;