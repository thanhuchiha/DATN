import React from 'react'
import { Form, Icon, Input, Button, Checkbox, message } from 'antd';
class NormalLoginForm extends React.Component {
    handleSubmit = e => {
        e.preventDefault();
        this.props.form.validateFields((err, values) => {
            if (!err) {
                message.loading('Logging In...', 2.5)
                .then(() => {
                    message.success('Now you are loggin in!');
                    console.log('Recevied values of form : ', values);
                })
                console.log('Received values of form: ', values);
            }
        });
    };

    render() {
        const { getFieldDecorator } = this.props.form;
        return (
            <article className="mw5 center bg-white shadow-5 br3 pa3 pa4-ns mv3 ba b--black-10">
                <Form onSubmit={this.handleSubmit} className="login-form">
                    <Form.Item>
                        {getFieldDecorator('username', {
                            rules: [{ required: true, message: 'Please input your username!' }],
                        })(
                            <Input
                                prefix={<Icon type="user" style={{ color: 'rgba(0,0,0,.25)' }} />}
                                placeholder="Username"
                            />,
                        )}
                    </Form.Item>
                    <Form.Item>
                        {getFieldDecorator('password', {
                            rules: [{ required: true, message: 'Please input your Password!' }],
                        })(
                            <Input
                                prefix={<Icon type="lock" style={{ color: 'rgba(0,0,0,.25)' }} />}
                                type="password"
                                placeholder="Password"
                            />,
                        )}
                    </Form.Item>
                    <Form.Item>
                        {getFieldDecorator('remember', {
                            valuePropName: 'checked',
                            initialValue: true,
                        })(<Checkbox>Remember me</Checkbox>)}
                        <a className="login-form-forgot" href="a.com">
                            Forgot password
          </a>
                        <Button type="primary" htmlType="submit" className="login-form-button">
                            Log in
          </Button>
                        Or <a href="a.com">register now!</a>
                    </Form.Item>
                </Form>
            </article>
        );
    }
}

const Login = Form.create({ name: 'normal_login' })(NormalLoginForm);

export default Login;