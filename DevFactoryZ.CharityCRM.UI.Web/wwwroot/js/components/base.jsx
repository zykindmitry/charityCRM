const e = React.createElement;

class InputComponent extends React.Component {
    constructor(props) {
        super(props);
        this.state = { ...props };
    }

    render() {
        return e(
            'input',
            {
                class: this.state.cssClass,
                id: this.state.id,
                type: this.state.type,
                placeholder: this.state.placeholder,
                autocomplete: this.state.autocomplete,
            },
            null
        );
    }
}

class SpanComponent extends React.Component {
    constructor(props) {
        super(props);
        this.state = { ...props };
    }

    render() {
        return e(
            'span',
            {
                class: this.state.cssClass,
            },
            this.state.value
        );
    }
}

class ButtonComponent extends React.Component {
    constructor(props) {
        super(props);
        this.state = { ...props };
    }

    render() {
        return e(
            'button',
            {
                type: this.state.type,
                class: this.state.cssClass,
                onClick: this.state.onClick,
            },
            this.state.caption
        );
    }
}
