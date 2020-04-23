import { httpPost } from '../common.js';

const loginURL = "/api/login";

const e = React.createElement;

let user = {
    Login: "",
    Password: "",
};

function validateUser(user) {
    return validateLength(user.login) && validateLength(user.password);
};

function validateLength(value) {
    return value.length > 0;
};

export class FormLogin extends React.Component {
    constructor(props) {
        super(props);
        this.state = { ...props };
    }
        
    login() {
        user.login = document.getElementById("username").value;
        user.password = document.getElementById("password").value;

        if (validateUser(user)) {
            httpPost(loginURL, user).then(result => alert(result));
        }
        else {
            alert("Не указано имя пользователя или пароль.");
        }
    }

    render() {
        return e(
            Login,
            {
                loginScript: this.login
            }
        );
    }
}

function Login(props) {
    return (
        <div className="card">
            <div className="card-header text-center">
                <a href="#">
                    <img className="logo-img" src="images/charity-logo-100-transp.png" alt="logo" />
                </a>
                <span className="splash-description">Авторизация</span>
            </div>
            <LoginBody loginScript={props.loginScript} />
            <div className="card-footer bg-white p-0">
                <div className="card-footer-item card-footer-item-bordered">
                    <a href="#" className="footer-link">Создать аккаунт</a>
                </div>
                <div className="card-footer-item card-footer-item-bordered">
                    <a href="#" className="footer-link">Забыл пароль</a>
                </div>
            </div>
        </div>
    );
};

function LoginBody(props){
    return (
        <div className="card-body">
            <FormInput id="username" type="text" placeholder="Имя пользователя (логин)" autoComplete="off" />
            <FormInput id="password" type="password" placeholder="Пароль" autoComplete="off" />
            <FormCheckBox description="Запомнить меня" />
            <FormButton type="submit" onClick={props.loginScript} caption="Войти" />
        </div>
    );
};

function FormCheckBox(props) {
    return (
        <div className="form-group">
            <label className="custom-control custom-checkbox">
                <input className="custom-control-input" type="checkbox" />
                <span className="custom-control-label">{props.description}</span>
            </label>
        </div>
    );
};

function FormInput(props) {
    return (
        <div className="form-group">
            <input className="form-control form-control-lg" id={props.id} type={props.type} placeholder={props.placeholder} autoComplete={props.autoComplete} />
        </div>
    );
};

function FormButton(props) {
    return (
        <button className="btn btn-primary btn-lg btn-block" type={props.type} onClick={props.onClick}>{props.caption}</button>
    );
};