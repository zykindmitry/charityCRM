import { httpGet } from '../common.js';

const rootWardCategoriesURL = "/api/wardcategories?onlyRoot=true";
const allWardsByCategoryURL = "/api/wards?categoryId=";
const wardsTableHeader = ["№ п/п", "ФИО", "Дата рождения", "Дата регистрации в фонде", "Категории"];

const e = React.createElement;

export class FormWards extends React.Component {
    constructor(props) {
        super(props);
        this.state = { ...props };
        this.getWards = this.getWards.bind(this);
    }

    async getRootWardCategories() {
        await httpGet(rootWardCategoriesURL).then(text => {
            this.setState({
                categories: JSON.parse(text)
            });
        });
    }

    async getWards(categoryId) {
        await httpGet(allWardsByCategoryURL + categoryId).then(text => {
            this.setState({
                wardsForView: this.parseWardListViewModelForTable(text),
                currentCategory: this.findCategoryById(categoryId)
            });
        });
    }

    parseWardListViewModelForTable(source) {
        return JSON.parse(source, function (key, value) {
            if (key.toLowerCase() == 'createdat' || key.toLowerCase() == 'birthdate') { return value };
            if (key.toLowerCase() == 'fullname') { return `${value.surName} ${value.firstName} ${value.middleName}`.trim() };
            if (key.toLowerCase() == 'wardcategories') { return value.map(item => (item.name)).toString() };
            return value;
        }).map((item, index) => ([Number(index) + 1, item.fullName, item.birthDate, item.createdAt, item.wardCategories]));
    }

    findCategoryById(id) {
        let category = this.state.categories.find(item => item.id == id);

        if (category == undefined) {
            category = this.state.categories
                .map(item => item.subCategories)
                .reduce((result, current) => result.concat(current), [])
                .find(subItem => subItem.id == id);
        }

        return category;
    }

    componentDidMount() {
        this.getRootWardCategories();
    }

    componentWillUnmount() {
        this.setState({
            categories: null,
            wardsForView: null,
            currentCategory: null
        });
    }

    render() {
        return e(
            Body,
            {
                getRoot: this.state.categories,
                getWards: this.getWards,
                wardsForView: this.state.wardsForView,
                currentCategory: this.state.currentCategory
            }
        );
    }
}

function Body(props){
    return (
        <div>
            <FormLeftSideBar formName="Меню" topLevelName="Категории" rootCategories={props.getRoot} getWards={props.getWards} />
            <FormWardContent firstLevelName="Категории" currentCategory={props.currentCategory} wards={props.wardsForView}/>
        </div>
    );
};

function FormLeftSideBar(props) {
    return (
        <div className="nav-left-sidebar sidebar-dark">
            <div className="menu-list">
                <div className="navbar navbar-expand-lg navbar-light">
                    <a className="d-xl-none d-lg-none" href="#">Dashboard</a>
                    <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarNav">
                        <ul className="navbar-nav flex-column">
                            <li className="nav-divider">
                                {props.formName}
                            </li>
                            <li className="nav-item">
                                <a className="nav-link active" href="#" data-toggle="collapse" aria-expanded="false" data-target="#submenu-1" aria-controls="submenu-1">
                                    <i className="fa fa-fw fa-user-circle"></i>
                                    {props.topLevelName}
                                    <span className="badge badge-success">6</span>
                                </a>
                            </li>
                            <FormWardCategoryMenu menuItems={props.rootCategories} getWards={props.getWards}/>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    );
};

function FormWardContent(props) {

    return (
        <div className="dashboard-wrapper">
            <div className="container-fluid  dashboard-content">
                <div className="row">
                    <div className="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                        <div className="page-header">
                            <h2 className="pageheader-title">Подопечные</h2>
                            <p className="pageheader-text">Proin placerat ante duiullam scelerisque a velit ac porta, fusce sit amet vestibulum mi. Morbi lobortis pulvinar quam.</p>
                            <div className="page-breadcrumb">
                                <nav aria-label="breadcrumb">
                                    <ol className="breadcrumb">
                                        <li className="breadcrumb-item">
                                            <a href="#" className="breadcrumb-link">
                                                {props.firstLevelName}
                                            </a>
                                        </li>
                                        <li className="breadcrumb-item">
                                            <a href="#" className="breadcrumb-link">
                                                {(props.currentCategory != null && props.currentCategory != undefined && props.currentCategory != '')
                                                    ? props.currentCategory.name
                                                    : ''
                                                }
                                            </a>
                                        </li>
                                        <li className="breadcrumb-item active" aria-current="page">
                                            {(props.currentCategory != null && props.currentCategory != undefined && props.currentCategory != '')
                                                ? 'Подопечные'
                                                : ''
                                            }                                            
                                        </li>
                                    </ol>
                                </nav>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                        <div className="card">
                            <div className="card-header" hidden>
                                <h5 className="mb-0">Data Tables - Print, Excel, CSV, PDF Buttons</h5>
                                <p>This example shows DataTables and the Buttons extension being used with the Bootstrap 4 framework providing the styling.</p>
                            </div>
                            <div className="card-body">
                                <div className="table-responsive">
                                    <FormDataTable tableId="example" tableHeader={wardsTableHeader} tableFooter={wardsTableHeader} tableData={props.wards} tableClass="table table-striped table-bordered second"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>            
        </div>
    );
};

function FormWardCategoryMenu(props) {
    let subMenu1;

    if (props.menuItems == undefined) {
        subMenu1 = <li key="0" className="nav-item">
            <a className="nav-link" href="#" data-toggle="collapse" aria-expanded="false" data-target="#submenu-1-2" aria-controls="submenu-1-2">
                -
            </a>
        </li>
    }
    else {
        subMenu1 = props.menuItems.map(item => (
            <li key={item.id} className="nav-item">
                <a className="nav-link" href="#" onClick={() => props.getWards(item.id)} data-toggle="collapse" aria-expanded="false" data-target="#submenu-1-2" aria-controls="submenu-1-2">
                    {item.name}
                </a>
                <div id="submenu-1-2" className="collapse submenu">
                    <ul className="nav flex-column">
                        {item.subCategories.map(subItem => (
                            <li key={subItem.id} className="nav-item">
                                <a className="nav-link" href="#" onClick={() => props.getWards(subItem.id)}>
                                    {subItem.name}
                                </a>
                            </li>
                        ))}
                    </ul>
                </div>
            </li>
        ));
    }

    return (
        <div id="submenu-1" className="collapse submenu">
            <ul className="nav flex-column">{subMenu1}</ul>
        </div>
    );
};

function FormDataTable(props) {
    let data;

    let header = props.tableHeader.map((item, index) => (<th key={index}>{item}</th>));
    let footer = props.tableHeader.map((item, index) => (<th key={index}>{item}</th>));

    if (props.tableData != undefined && props.tableData != null && props.tableData != "") {
        data = props.tableData.map((item, index) => (
            <tr key={index}>
                {item.map((subItem, subIndex) => (
                    <td key={subIndex}>{subItem}</td>                    
                ))}
            </tr>
        ));
    }

    return (
        <table id={props.tableId} className={props.tableClass}>
            <thead>
                <tr>
                    {header}
                </tr>
            </thead>
            <tbody>
                    {data}
            </tbody>
            <tfoot>
                <tr>
                    {footer}
                </tr>
            </tfoot>
        </table>   
    );
};
