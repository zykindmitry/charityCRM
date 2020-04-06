function LoginComponent(props) {
    return (
        <div class="card">
            <LoginHeaderComponent headerImageClass="logo-img" headerImageSrc="images/charity-logo-100-transp.png" headerDescription="Авторизация" />
            <LoginBodyComponent />
            <LoginFooterComponent createAccountRef="#" restorePasswordRef="#" />
        </div>
        );
};

function LoginHeaderComponent(props) {
    return (
        <div class="card-header text-center">
            <a href="react.html">
                <img class={props.headerImageClass} src={props.headerImageSrc} alt="logo" />
            </a>
            <SpanComponent cssClass="splash-description" value={props.headerDescription} />
        </div>
        );
};

function LoginBodyComponent(props){
    return (
        <div class="card-body">
            <div class="form-group">
                <InputComponent cssClass="form-control form-control-lg" id="username" type="text" placeholder="Имя пользователя (логин)" autocomplete="off" />
            </div>
            <div class="form-group">
                <InputComponent cssClass="form-control form-control-lg" id="password" type="password" placeholder="Пароль" autocomplete="off" />
            </div>
            <div class="form-group">
                <CustomCheckBoxComponent cssClass="custom-control custom-checkbox" description="Запомнить меня" />
            </div>
            <ButtonComponent type="submit" cssClass="btn btn-primary btn-lg btn-block" onClick={login} caption="Войти"/>
        </div>
        );
};

function LoginFooterComponent(props) {
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

ReactDOM.render(
    <LoginComponent />,
    document.getElementById('root')
);