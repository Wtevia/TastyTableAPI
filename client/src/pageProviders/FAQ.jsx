import { useSelector } from 'react-redux';
import React, { useEffect } from 'react';
import useChangePage from 'hooks/useChangePage';
import PageContainer from 'components/PageContainer/PageContainer';
import * as PAGES from 'constants/pages';
import FAQPage from 'pages/FAQ';

const FAQ = () => {
    const user = useSelector(({ user }) => user);
    const changePage = useChangePage();
    useEffect(() => {
        
        if (!user.isAuthorized) {
            changePage({
                path: `/${PAGES.LOGIN}`
            });
        }
    }, [user.isAuthorized]);

    return (
        !user.isAuthorized
        ? null
        : (
            <PageContainer>
                <FAQPage />
            </PageContainer>
        )
    );
};

export default FAQ;