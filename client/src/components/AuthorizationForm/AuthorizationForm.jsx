import React, { useState, useEffect } from "react";
import { useDispatch, useSelector } from 'react-redux';
import { ContainerStyled, LoadingContainerStyled, TabContentStyled, InputFieldStyled, FullWidthStyled, PaddingTop3xStyled, PaddingTop4xStyled, ActionsContainerStyled, ActionItemStyled } from './AuthorizationFormStyles';
import Tab from 'components/Tab';
import TabContent from 'components/TabContent';
import Tabs from 'components/Tabs';
import TabsContent from 'components/TabsContent';
import TextField from 'components/TextField';
import Typography from 'components/Typography';
import Button from 'components/Button';
import PropTypes from 'prop-types';
import {
  fetchSignIn,
  fetchSignUpAndSignIn,
} from 'app/actions/user';


const AVAILABLE_TABS = {
    login: 'login',
    register: 'register',
  };
  
  const initialState = {
    login: '',
    password: '',
    selectedTab: AVAILABLE_TABS.login,
  };

const AuthorizationForm = ({
    onSuccess,
}) => {
  
  const dispatch = useDispatch();
  const user = useSelector(({ user }) => user);
  // const { formatMessage } = useIntl();
  const [state, setState] = useState(initialState);

  useEffect(() => {
    if (user.isAuthorized && onSuccess) {
      onSuccess();
    }
  }, [user.isAuthorized]);

    return (
    <ContainerStyled>
      {user.isFetchingUser && (
        <LoadingContainerStyled>
          <Typography>
            Loading...
          </Typography>
        </LoadingContainerStyled>
      )}
      {!user.isFetchingUser && (
        <>
          <Tabs
            fullWidth
            onChange={(event, newValue) => {
              setState({
                ...initialState,
                selectedTab: newValue,
              });
            }}
            value={state.selectedTab}
          >
            <Tab
              // label={formatMessage({
              //   id: 'signIn',
              // })}
              label='signIn'
              value={AVAILABLE_TABS.login}
            />
            <Tab
              // label={formatMessage({
              //   id: 'signUp',
              // })}
              label='signUp'
              value={AVAILABLE_TABS.register}
            />
          </Tabs>
          <TabsContent value={state.selectedTab}>
            <TabContent
              key={AVAILABLE_TABS.login}
              value={AVAILABLE_TABS.login}
            >
              <TabContentStyled>
                <InputFieldStyled>
                  <TextField
                    fullWidth
                    key="login"
                    // label={formatMessage({
                    //   id: 'login',
                    // })}
                    label='login'
                    onChange={({ target }) => setState(prevState => ({
                      ...prevState,
                      login: target.value,
                    }))}
                    value={state.login}
                  />
                </InputFieldStyled>
                <FullWidthStyled>
                  <PaddingTop3xStyled>
                    <InputFieldStyled>
                      <TextField
                        fullWidth
                        inputType="password"
                        key="password"
                        // label={formatMessage({
                        //   id: 'password',
                        // })}
                        label='password'
                        onChange={({ target }) => setState(prevState => ({
                          ...prevState,
                          password: target.value,
                        }))}
                        value={state.password}
                      />
                    </InputFieldStyled>
                  </PaddingTop3xStyled>
                </FullWidthStyled>
                <FullWidthStyled>
                  <PaddingTop4xStyled>
                    <ActionsContainerStyled>
                      <ActionItemStyled>
                        <Button
                          disabled={user.isFetchingUser}
                          fullWidth
                          onClick={() => dispatch(fetchSignIn({ // TODO
                            login: state.login,
                            password: state.password,
                          }))}
                          variant="outlined"
                        >
                          <Typography variant="button">
                            {/* {formatMessage({ id: 'signIn' })} */}
                            {'signIn'}
                          </Typography>
                        </Button>
                      </ActionItemStyled>
                    </ActionsContainerStyled>
                  </PaddingTop4xStyled>
                </FullWidthStyled>
              </TabContentStyled>
            </TabContent>
            <TabContent
              key={AVAILABLE_TABS.register}
              value={AVAILABLE_TABS.register}
            >
              <TabContentStyled>
                <FullWidthStyled>
                  <InputFieldStyled>
                    <TextField
                      fullWidth
                      key="login"
                      // label={formatMessage({
                      //   id: 'login',
                      // })}
                      label='login'
                      onChange={({ target }) => setState(prevState => ({
                        ...prevState,
                        login: target.value,
                      }))}
                      value={state.login}
                    />
                  </InputFieldStyled>
                </FullWidthStyled>
                <FullWidthStyled>
                  <PaddingTop3xStyled>
                    <InputFieldStyled>
                      <TextField
                        fullWidth
                        inputType="password"
                        key="password"
                        // label={formatMessage({
                        //   id: 'password',
                        // })}
                        label='password'
                        onChange={({ target }) => setState(prevState => ({
                          ...prevState,
                          password: target.value,
                        }))}
                        value={state.password}
                      />
                    </InputFieldStyled>
                  </PaddingTop3xStyled>
                </FullWidthStyled>
                <FullWidthStyled>
                  <PaddingTop4xStyled>
                    <ActionsContainerStyled>
                      <ActionItemStyled>
                        <Button
                          disabled={user.isFetchingSignUp}
                          fullWidth
                          onClick={() => dispatch(fetchSignUpAndSignIn({ // TODO
                            login: state.login,
                            password: state.password,
                          }))}
                          variant="outlined"
                        >
                          <Typography variant="button">
                            {/* {formatMessage({ id: 'signUp' })} */}
                            {'signUp'}
                          </Typography>
                        </Button>
                      </ActionItemStyled>
                    </ActionsContainerStyled>
                  </PaddingTop4xStyled>
                </FullWidthStyled>
              </TabContentStyled>
            </TabContent>
          </TabsContent>
        </>
      )}
    </ContainerStyled>
    );
};

AuthorizationForm.propTypes = {
  onSuccess: PropTypes.func,
};

export default AuthorizationForm;