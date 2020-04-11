import { login } from '../login.js';

const e = React.createElement;

export class LoginForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = { ...props };
    }

    render() {
        return e(
            Login,
            {
                loginScript: login
            }
        );
    }
}

function Login(props) {
    return (
        <div class="card">
            <LoginHeader headerRef="#" headerImageClass="logo-img" headerImageSrc="images/charity-logo-100-transp.png" headerDescription="Авторизация" />            
            <LoginBody loginScript={props.loginScript} />
            <LoginFooter createAccountRef="#" restorePasswordRef="#" />
        </div>
    );
};

function LoginHeader(props) {
    return (
        <div class="card-header text-center">
            <a href={props.headerRef}>
                <img class={props.headerImageClass} src={props.headerImageSrc} alt="logo" />
            </a>
            <span class="splash-description">{props.headerDescription}</span>
        </div>
    );
};

function LoginBody(props){
    return (
        <div class="card-body">
            <div class="form-group">
                <input class="form-control form-control-lg" id="username" type="text" placeholder="Имя пользователя (логин)" autocomplete="off" />
            </div>
            <div class="form-group">
                <input class="form-control form-control-lg" id="password" type="password" placeholder="Пароль" autocomplete="off" />
            </div>
            <div class="form-group">
                <CustomCheckBox description="Запомнить меня" />
            </div>
            <button type="submit" class="btn btn-primary btn-lg btn-block" onClick={props.loginScript}>Войти</button>
        </div>
    );
};

function LoginFooter(props) {
    return (
        <div class="card-footer bg-white p-0  ">
            <div class="card-footer-item card-footer-item-bordered">
                <a href={props.createAccountRef} class="footer-link">Создать аккаунт</a>
            </div>
            <div class="card-footer-item card-footer-item-bordered">
                <a href={props.restorePasswordRef} class="footer-link">Забыл пароль</a>
            </div>
        </div>
    );
};

function CustomCheckBox(props) {
    return (
        <label class="custom-control custom-checkbox">
            <input class="custom-control-input" type="checkbox" />
            <span class="custom-control-label">{props.description}</span>
        </label>
    );
};