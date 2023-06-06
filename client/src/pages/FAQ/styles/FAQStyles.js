import styled from 'styled-components';

const sizes = {
    mobileS: '320px',
    mobileM: '375px',
    mobileL: '425px',
    tablet: '768px',
    laptop: '1024px',
};

export const devices = {
    mobileS: `(min-width: ${sizes.mobileS})`,
    mobileM: `(min-width: ${sizes.mobileM})`,
    mobileL: `(min-width: ${sizes.mobileL})`,
    tablet: `(min-width: ${sizes.tablet})`,
    laptop: `(min-width: ${sizes.laptop})`,
};

export const AccordionSection = styled.div`
//   flex-direction: column;
  align-items: center;
  justify-content: center;
//   position: relative;
  height: 100vh;
  background: ##1F1D2B;
  margin-left: 6rem;
  margin-top: 3rem;

  @media ${devices.mobileS} {
    margin-left: 0.5rem;
  }

  @media ${devices.mobileL} {
    margin-left: 2rem;
  }

  @media ${devices.tablet} {
    margin-left: 4rem;
  }
`;

export const Container = styled.div`
//   position: absolute;
  top: 30%;
  box-shadow: 2px 10px 35px 1px rgba(153, 153, 153, 0.3);
 width: 70vw;
`;

export const Wrap = styled.div`
  background: #1F1D2B;
  color: #fff;
  display: flex;
  justify-content: space-between;
  align-items: center;
  width: 100%;
  text-align: center;
  cursor: pointer;
  h1 {
    padding: 2rem;
    font-size: 1.2rem;
  }
  span {
    margin-right: 1.5rem;
  }
`;

export const Dropdown = styled.div`
  background: #1F1D2B;
  color: #F17E6C;
  width: 100%;
  height: 100px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  border-bottom: 1px solid #F17E6C;
  border-top: 1px solid #F17E6C;
  p {
    font-size: 1rem;
    padding: 2rem;
  }
`;