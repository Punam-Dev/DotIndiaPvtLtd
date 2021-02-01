using DotIndiaPvtLtd.Dtos;
using DotIndiaPvtLtd.Models;
using DotIndiaPvtLtd.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotIndiaPvtLtd.Controllers
{
    [Authorize]
    public class FormController : Controller
    {
        private readonly IFormsRepository formsRepository;
        private string LoggedInUserID;
        public FormController(IFormsRepository formsRepository)
        {
            this.formsRepository = formsRepository;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Route("Form/Create/{id}")]
        [HttpGet]
        public async Task<IActionResult> Create(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var Form = await formsRepository.GetFormByID(Convert.ToInt64(id));

                CreateFormDto createFormDto = new CreateFormDto();
                createFormDto.FormsID = Form.FormsID;
                createFormDto.FirstName = Form.FirstName;
                createFormDto.LastName = Form.LastName;
                createFormDto.Email = Form.Email;
                createFormDto.SSN = Form.SSN;
                createFormDto.Phone = Form.Phone;
                createFormDto.BankName = Form.BankName;
                createFormDto.AccountNo = Form.AccountNo;
                createFormDto.LoanAmount = Form.LoanAmount;
                createFormDto.Address = Form.Address;
                createFormDto.City = Form.City;
                createFormDto.State = Form.State;
                createFormDto.Zip = Form.Zip;
                createFormDto.DOB = Form.DOB;
                createFormDto.LicenceNo = Form.LicenceNo;
                createFormDto.LicenceState = Form.LicenceState;
                createFormDto.IP = Form.IP;

                List<FormQueryDto> formQueryDtos = new List<FormQueryDto>()
                {
                    new FormQueryDto {FormQueryText = "First Name", IsChecked = false },
                    new FormQueryDto {FormQueryText = "Last Name", IsChecked = false },
                    new FormQueryDto {FormQueryText = "Email", IsChecked = false },
                    new FormQueryDto {FormQueryText = "SSN", IsChecked = false },
                    new FormQueryDto {FormQueryText = "Phone", IsChecked = false },
                    new FormQueryDto {FormQueryText = "Bank Name", IsChecked = false },
                    new FormQueryDto {FormQueryText = "A/C No", IsChecked = false },
                    new FormQueryDto {FormQueryText = "Loan Amount", IsChecked = false },
                    new FormQueryDto {FormQueryText = "City", IsChecked = false },
                    new FormQueryDto {FormQueryText = "State", IsChecked = false },
                    new FormQueryDto {FormQueryText = "ZIP", IsChecked = false },
                    new FormQueryDto {FormQueryText = "Date of Birth", IsChecked = false },
                    new FormQueryDto {FormQueryText = "Licence No.", IsChecked = false },
                    new FormQueryDto {FormQueryText = "Licence State", IsChecked = false },
                    new FormQueryDto {FormQueryText = "IP", IsChecked = false },
                };

                createFormDto.formQueryDtos = formQueryDtos;


                return View(createFormDto);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFormDto createFormDto, string submit, string id)
        {
            Forms forms = new Forms();

            forms.FirstName = createFormDto.FirstName;
            forms.LastName = createFormDto.LastName;
            forms.Email = createFormDto.Email;
            forms.SSN = createFormDto.SSN;
            forms.Phone = createFormDto.Phone;
            forms.BankName = createFormDto.BankName;
            forms.AccountNo = createFormDto.AccountNo;
            forms.LoanAmount = createFormDto.LoanAmount;
            forms.Address = createFormDto.Address;
            forms.City = createFormDto.City;
            forms.State = createFormDto.State;
            forms.Zip = createFormDto.Zip;
            forms.DOB = createFormDto.DOB;
            forms.LicenceNo = createFormDto.LicenceNo;
            forms.LicenceState = createFormDto.LicenceState;
            forms.IP = createFormDto.IP;

            if (submit == "Submit")
            {
                forms.FormIsSubmit = true;
            }
            else
            {
                forms.FormIsSubmit = false;
            }
            forms.UserCreatedDate = DateTime.Now;

            if (TempData.ContainsKey("UserID"))
            {
                LoggedInUserID = TempData.Peek("UserID").ToString();
            }

            if (!string.IsNullOrEmpty(LoggedInUserID))
            {
                forms.UserCreatedByUserID = LoggedInUserID;
            }
            else
            {
                return RedirectToAction("LogIn", "Account");
            }

            var createdForm = await formsRepository.CreateForm(forms);

            return View(createFormDto);
        }

        [Route("Form/Index/{id}")]
        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            bool? isSubmit;
            if (id.ToLower() == "save")
            {
                isSubmit = false;
            }
            else if (id.ToLower() == "submit")
            {
                isSubmit = true;
            }
            else
            {
                isSubmit = null;
            }

            if (TempData.ContainsKey("UserID"))
            {
                LoggedInUserID = TempData.Peek("UserID").ToString();
            }

            var forms = await formsRepository.GetForms(isSubmit, LoggedInUserID);
            return View(forms);
        }

        [HttpPost]
        public IActionResult FormQuery(CreateFormDto createFormDto, string id)
        {
            List<FormQuery> formQueryList = new List<FormQuery>();

            foreach (var item in createFormDto.formQueryDtos)
            {
                if (item.IsChecked)
                {
                    FormQuery formQuery = new FormQuery();

                    formQuery.FormQueryText = Convert.ToString(item.FormQueryText);
                    formQuery.FormQueryStatus = "Pending";
                    formQuery.FormQueryCreatedDate = DateTime.Now;

                    if (TempData.ContainsKey("UserID"))
                    {
                        LoggedInUserID = TempData.Peek("UserID").ToString();
                    }

                    if (!string.IsNullOrEmpty(LoggedInUserID))
                    {
                        formQuery.FormQueryCreatedByUserID = LoggedInUserID;
                    }
                    else
                    {
                        return RedirectToAction("LogIn", "Account");
                    }

                    formQueryList.Add(formQuery);
                }
            }

            formsRepository.CreateFormQuery(formQueryList);

            return RedirectToAction("Create", "Form", new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> Query()
        {
            if (TempData.ContainsKey("UserID"))
            {
                LoggedInUserID = TempData.Peek("UserID").ToString();
            }

            if (!string.IsNullOrEmpty(LoggedInUserID))
            {
                var formQuery = await formsRepository.GetFormQueries(LoggedInUserID);
                return View(formQuery);
            }
            return RedirectToAction("LogIn", "Account");
        }

        [HttpGet]
        public IActionResult Report()
        {
            return View();
        }
    }
}
